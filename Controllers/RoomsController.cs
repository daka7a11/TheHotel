using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using TheHotel.Common;
using TheHotel.EmailSender;
using TheHotel.EmailSender.ViewRender;
using TheHotel.Mapping;
using TheHotel.Services.ClientRooms;
using TheHotel.Services.Rooms;
using TheHotel.ViewModels.Rooms;

namespace TheHotel.Controllers
{
    public class RoomsController : Controller
    {
        private readonly IRoomsService roomsService;
        private readonly IClientRoomsService clientRoomsService;
        private readonly IViewRenderService viewRenderService;
        private readonly IMailService mailService;

        public RoomsController(IRoomsService roomsService,
            IClientRoomsService clientRoomsService,
            IViewRenderService viewRenderService,
            IMailService mailService)
        {
            this.roomsService = roomsService;
            this.clientRoomsService = clientRoomsService;
            this.viewRenderService = viewRenderService;
            this.mailService = mailService;
        }

        public IActionResult All(DateTime? accommodationDate, DateTime? departureDate, int? numGuests)
        {
            var model = new AllViewModel();

            if (accommodationDate == null || departureDate == null || departureDate.Value.Date <= accommodationDate.Value.Date)
            {
                var allRooms = this.roomsService.GetAll()
                    .Select(x => new AllRoomsViewModel()
                    {
                        Id = x.Id,
                        Size = x.Size,
                        RoomType = x.RoomType.Type,
                        MaxGuests = x.RoomType.MaxGuests,
                        Floor = x.Floor,
                        Price = x.Price,
                        Description = x.Description,
                    });

                model.AllRoomsViewModel = allRooms;

                return this.View(model);
            }

            var rooms = roomsService.GetAll()
                .Where(x => GlobalMethods.IsRoomAvailable(x, accommodationDate, departureDate))
                .Select(x => new AllRoomsViewModel()
                {
                    Id = x.Id,
                    Size = x.Size,
                    RoomType = x.RoomType.Type,
                    Floor = x.Floor,
                    Price = x.Price,
                    Description = x.Description,
                    FirstImgUrl = x.Images.FirstOrDefault()?.Url,
                    MaxGuests = x.RoomType.MaxGuests,
                });

            model = new AllViewModel()
            {
                AllRoomsViewModel = rooms,
                AvailableRoomsViewModel = new AvailableRoomsViewModel()
                {
                    AccommodationDate = accommodationDate,
                    DepartureDate = departureDate,
                }
            };

            return this.View(model);
        }

        public IActionResult ClientPin(int roomId)
        {
            var model = new HireRoomViewModel()
            {
                RoomId = roomId,
            };
            return this.View(model);
        }

        [HttpPost]
        public IActionResult ClientPin(HireRoomViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            return this.RedirectToAction("Tenancy", "ClientRooms", new { roomId = model.RoomId, clientPin = model.PersonalIdentityNumber });
        }

        public IActionResult Details(int roomId)
        {
            var room = roomsService.GetById<RoomDetailsViewModel>(roomId);

            if (room == null)
            {
                return this.Redirect($"RoomError?roomId={roomId}");
            }

            return this.View(room);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRole)]
        public IActionResult AddImage(int roomId)
        {
            var model = new AddImageToRoomViewModel { RoomId = roomId };

            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRole)]
        [HttpPost]
        public async Task<IActionResult> AddImage(AddImageToRoomViewModel viewModel)
        {
            var room = roomsService.GetById(viewModel.RoomId);

            if (room == null)
            {
                ModelState.AddModelError("", "Invalid room id.");
                return this.View(viewModel);
            }

            await roomsService.AddImageToRoomAsync(viewModel.RoomId, viewModel.ImageUrl);

            return this.Redirect($"/Rooms/Details?roomId={viewModel.RoomId}");
        }

        public IActionResult Edit(int roomId)
        {
            var room = roomsService.GetById(roomId);
            var input = AutoMapperConfig.MapperInstance.Map<EditRoomViewModel>(room);
            input.RoomTypes = roomsService.GetAllRoomTypes();
            return this.View(input);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRole)]
        [HttpPost]
        public IActionResult Edit(EditRoomViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            roomsService.Edit(model.Id,model);

            return this.Redirect($"Details?roomId={model.Id}");
        }


        [Authorize(Roles = GlobalConstants.AdministratorRole)]
        public IActionResult Delete(int roomId)
        {
            roomsService.Delete(roomId);
            return this.Redirect("/Rooms/All");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRole)]
        public IActionResult Undelete()
        {
            var model = roomsService.GetDeleted<UndeleteViewModel>();
            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRole)]
        [HttpPost]
        public IActionResult Undelete(int roomId)
        {
            roomsService.Undelete(roomId);
            return this.Redirect("/Rooms/All");
        }
    }
}
