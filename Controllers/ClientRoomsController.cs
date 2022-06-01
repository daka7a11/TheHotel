using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheHotel.Common;
using TheHotel.Data.Models;
using TheHotel.EmailSender;
using TheHotel.EmailSender.ViewRender;
using TheHotel.Mapping;
using TheHotel.Services.ClientRooms;
using TheHotel.Services.Clients;
using TheHotel.Services.Offers;
using TheHotel.Services.Rooms;
using TheHotel.ViewModels;
using TheHotel.ViewModels.ClientRooms;
using TheHotel.ViewModels.Clients;
using TheHotel.ViewModels.Rooms;

namespace TheHotel.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRole + "," + GlobalConstants.ReceptionistRole)]
    public class ClientRoomsController : Controller
    {
        private readonly IClientRoomsService clientRoomsService;
        private readonly IClientsService clientsService;
        private readonly IRoomsService roomsService;
        private readonly IOffersService offersService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMailService mailService;
        private readonly IViewRenderService renderService;

        public ClientRoomsController(IClientRoomsService clientRoomsService,
            IClientsService clientsService,
            IRoomsService roomsService,
            IOffersService offersService,
            UserManager<ApplicationUser> userManager,
            IMailService mailService,
            IViewRenderService renderService)
        {
            this.clientRoomsService = clientRoomsService;
            this.clientsService = clientsService;
            this.roomsService = roomsService;
            this.offersService = offersService;
            this.userManager = userManager;
            this.mailService = mailService;
            this.renderService = renderService;
        }

        [AllowAnonymous]
        public IActionResult Tenancy(int roomId, string clientPin)
        {
            var client = this.clientsService.GetClientByPIN(clientPin);
            var model = new TenancyViewModel() { RoomId = roomId, PersonalIdentityNumber = clientPin };
            if (client != null)
            {
                model = AutoMapperConfig.MapperInstance.Map<TenancyViewModel>(client);
            }

            return this.View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Tenancy(TenancyViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var selectedRoom = this.roomsService.GetById(model.RoomId);

            if (selectedRoom == null)
            {
                this.ModelState.AddModelError(string.Empty, string.Format(GlobalConstants.InvalidRoomErrorMsg, model.RoomId));
                return this.View(model);
            }

            foreach (var hireDate in selectedRoom.HireDates.Where(x => x.IsConfirmed == true && x.IsDeleted == false))
            {
                if (model.AccommodationDate < hireDate.AccommodationDate && model.DepartureDate > hireDate.AccommodationDate)
                {
                    this.ModelState.AddModelError(string.Empty, string.Format(GlobalConstants.InvalidHireDateErrorMsg, model.RoomId, hireDate.AccommodationDate.ToString("d"), hireDate.DepartureDate.ToString("d")));
                    return this.View(model);
                }

                if (model.AccommodationDate >= hireDate.AccommodationDate && model.AccommodationDate.Value.Date < hireDate.DepartureDate.Date)
                {
                    this.ModelState.AddModelError(string.Empty, string.Format(GlobalConstants.InvalidHireDateErrorMsg, model.RoomId, hireDate.AccommodationDate.ToString("d"), hireDate.DepartureDate.ToString("d")));
                    return this.View(model);
                }
            }


            if (this.clientsService.GetClientByPIN(model.PersonalIdentityNumber) == null)
            {
                if (clientsService.IsEmailExist(model.Email))
                {
                    this.ModelState.AddModelError(string.Empty, string.Format(GlobalConstants.EmailExistErrorMsg, model.Email));
                    return this.View(model);
                }

                var newClient = AutoMapperConfig.MapperInstance.Map<Client>(model);

                await this.clientsService.AddAsync(newClient);
            }

            var currClient = this.clientsService.GetClientByPIN(model.PersonalIdentityNumber);

            if (model.AccommodationDate == null)
            {
                this.ModelState.AddModelError(string.Empty, string.Format(GlobalConstants.InvalidTenancyDateErrorMsg, "Accommodation"));
                return this.View(model);
            }

            if (model.DepartureDate == null)
            {
                this.ModelState.AddModelError(string.Empty, string.Format(GlobalConstants.InvalidTenancyDateErrorMsg, "Departure"));
                return this.View(model);
            }

            bool isEmployee = User.IsInRole("Administrator") || User.IsInRole("Receptionist");

            var clientRoom = AutoMapperConfig.MapperInstance.Map<ClientRoom>(model);
            clientRoom.ClientId = currClient.Id;

            if (isEmployee)
            {
                clientRoom.IsConfirmed = true;
                clientRoom.EmployeeId = userManager.GetUserAsync(User).Result.Id;
                clientRoom.CreatedOn = DateTime.UtcNow;
            }
            else
            {
                clientRoom.RequestDate = DateTime.UtcNow;
            }

            await this.clientRoomsService.AddAsync(clientRoom);

            if (isEmployee)
            {
                return this.Redirect($"/ClientRooms/ReservationDetails?clientRoomId={clientRoom.Id}");
            }
            else
            {
                var reservationModel = AutoMapperConfig.MapperInstance.Map<MailReservationViewModel>(clientRoom);
                reservationModel.TotalDiscount = offersService.GetTotalDiscount(clientRoom);
                reservationModel.TotalPrice = ((int)(clientRoom.DepartureDate - clientRoom.AccommodationDate).TotalDays * clientRoom.Room.Price) - reservationModel.TotalDiscount;

                var mailBody = await renderService.RenderToStringAsync("MailReservation", reservationModel);

                MailRequest mailRequest = new MailRequest()
                {
                    ToEmail = currClient.Email,
                    Subject = GlobalConstants.ReservationRequest,
                    Body = mailBody,
                };

                await mailService.SendEmailAsync(mailRequest);

                TempData.Add("SuccessfullyRequest", currClient.Email);

                return this.Redirect($"/");
            }
        }

        public IActionResult RoomRequests()
        {
            var roomRequests = clientRoomsService
                .GetNonConfirmedReservations<RoomRequestViewModel>();

            return this.View(roomRequests);
        }

        public IActionResult RequestDetails(int clientRoomId)
        {
            var model = clientRoomsService.GetById<RequestDetailsViewModel>(clientRoomId);
            var room = roomsService.GetById(model.RoomId);
            model.IsStillAvailable = GlobalMethods.IsRoomAvailable(room, model.AccommodationDate, model.DepartureDate);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AcceptRequest(int clientRoomId)
        {
            var clientRoom = clientRoomsService.GetById(clientRoomId);
            var room = roomsService.GetById(clientRoom.RoomId);
            if (!GlobalMethods.IsRoomAvailable(room, clientRoom.AccommodationDate, clientRoom.DepartureDate))
            {
                TempData["ErrorMsg"] = GlobalConstants.RoomAlreadyHiredErrorMsg;
                return RedirectToAction(nameof(RequestDetails), "ClientRooms", new { clientRoomId });
            }

            var employeeId = userManager.GetUserId(User);

            await clientRoomsService.ConfirmRequestAsync(clientRoomId, employeeId);

            return this.Redirect($"/Rooms/Details?roomId={room.Id}");

        }

        [HttpPost]
        public async Task<IActionResult> RejectRequest(int clientRoomId)
        {
            var employeeId = userManager.GetUserId(User);

            await clientRoomsService.DeleteRequestAsync(clientRoomId, employeeId);
            return this.RedirectToAction(nameof(RoomRequests));
        }

        public IActionResult ReservationDetails(int clientRoomId)
        {
            var model = clientRoomsService.GetById<ReservationDetailsViewModel>(clientRoomId);

            return this.View(model);
        }

        public IActionResult Delete(int clientRoomId)
        {
            clientRoomsService.Delete(clientRoomId);
            return this.Redirect($"/ClientRooms/ReservationDetails?clientRoomId={clientRoomId}");
        }

        public IActionResult RejectedRequests()
        {
            var rejectedRequests = clientRoomsService.GetRejectedRequests<DeletedClientRoomsViewModel>();

            return View(rejectedRequests);
        }

        [HttpPost]
        public IActionResult RejectedRequests(int clientRoomId)
        {
            var rejectedRequests = clientRoomsService.GetRejectedRequests<DeletedClientRoomsViewModel>();

            return View(rejectedRequests);
        }

        public IActionResult DeletedReservations()
        {
            var deletedReservations = clientRoomsService.GetDeletedReservations<DeletedClientRoomsViewModel>();

            return View(deletedReservations);
        }

        [HttpPost]
        public IActionResult DeletedReservations(int clientRoomId)
        {
            var rejectedRequests = clientRoomsService.GetRejectedRequests<DeletedClientRoomsViewModel>();

            return View(rejectedRequests);
        }
    }
}
