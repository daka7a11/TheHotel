using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheHotel.Common;
using TheHotel.Data.Models;
using TheHotel.EmailSender;
using TheHotel.Mapping;
using TheHotel.Services.ClientRooms;
using TheHotel.Services.Clients;
using TheHotel.Services.Rooms;
using TheHotel.ViewModels.ClientRooms;
using TheHotel.ViewModels.Clients;
using TheHotel.ViewModels.Rooms;

namespace TheHotel.Controllers
{
    public class ClientRoomsController : Controller
    {
        private readonly IClientRoomsService clientRoomsService;
        private readonly IClientsService clientsService;
        private readonly IRoomsService roomsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMailService mailService;

        public ClientRoomsController(IClientRoomsService clientRoomsService,
            IClientsService clientsService,
            IRoomsService roomsService,
            UserManager<ApplicationUser> userManager,
            IMailService mailService)
        {
            this.clientRoomsService = clientRoomsService;
            this.clientsService = clientsService;
            this.roomsService = roomsService;
            this.userManager = userManager;
            this.mailService = mailService;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult RoomRequests()
        {
            var roomRequests = clientRoomsService
                .GetNonConfirmedReservations<RoomRequestViewModel>();

            return this.View(roomRequests);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult RequestDetails(int clientRoomId)
        {
            var model = clientRoomsService.GetById<RequestDetailsViewModel>(clientRoomId);
            var room = roomsService.GetById(model.RoomId);
            model.IsStillAvailable = GlobalMethods.IsRoomAvailable(room, model.AccommodationDate, model.DepartureDate);
            return this.View(model);
        }

        [HttpGet]
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

            foreach (var hireDate in selectedRoom.HireDates.Where(x => x.IsConfirmed == true))
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
            }
            else
            {
                clientRoom.RequestDate = DateTime.UtcNow;
            }

            await this.clientRoomsService.AddAsync(clientRoom);

            if (isEmployee)
            {
                return this.Redirect(
                    $"/ClientRooms/Success?clientName={currClient.FirstName}&roomId={model.RoomId}" +
                    $"&accDate={model.AccommodationDate.Value.ToString("dd.MM.yyyy")}&depDate={model.DepartureDate.Value.ToString("dd.MM.yyyy")}");
            }
            else
            {
                MailRequest mailRequest = new MailRequest()
                {
                    ToEmail = currClient.Email,
                    Subject = GlobalConstants.ReservationRequest,
                    Body = "Бла бла",
                };

                await mailService.SendEmailAsync(mailRequest);

                return this.Redirect("/");
            }
        }
    }
}
