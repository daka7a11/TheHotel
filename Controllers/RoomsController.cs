using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheHotel.Common;
using TheHotel.Data;
using TheHotel.Data.Models;
using TheHotel.EmailSender;
using TheHotel.EmailSender.ViewRender;
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

        public IActionResult All()
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

            var model = new AllViewModel()
            {
                AllRoomsViewModel = allRooms,
            };

            return this.View(model);
        }

        [HttpPost]
        public IActionResult All(DateTime? accommodationDate, DateTime? departureDate, int? numGuests)
        {
            if (accommodationDate == null || departureDate == null )
            {
                return Redirect("/Rooms/All");
            }

            var rooms = roomsService.GetAll()
                .Where(x => GlobalMethods.IsRoomAvailable(x,accommodationDate,departureDate))
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

            var model = new AllViewModel()
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

        public IActionResult AddImage(int roomId)
        {
            var model = new AddImageToRoomViewModel { RoomId = roomId};

            return this.View(model);
        }

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
    }
}
