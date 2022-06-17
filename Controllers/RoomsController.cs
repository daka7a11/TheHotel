using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TheHotel.Common;
using TheHotel.Mapping;
using TheHotel.Services.Rooms;
using TheHotel.ViewModels.Rooms;

namespace TheHotel.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class RoomsController : Controller
    {
        private readonly IRoomsService roomsService;

        public RoomsController(IRoomsService roomsService)
        {
            this.roomsService = roomsService;
        }

        [AllowAnonymous]
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
                        Floor = x.Floor,
                        Price = x.Price,
                        Description = x.Description,
                        MaxGuests = x.RoomType.MaxGuests,
                        FirstImgSrc = roomsService.GetRoomImages(x.Id).FirstOrDefault(),
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
                    MaxGuests = x.RoomType.MaxGuests,
                    FirstImgSrc = roomsService.GetRoomImages(x.Id).FirstOrDefault(),
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

        [AllowAnonymous]
        public IActionResult ClientPin(int roomId)
        {
            var model = new HireRoomViewModel()
            {
                RoomId = roomId,
            };
            return this.View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ClientPin(HireRoomViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            return this.RedirectToAction("Tenancy", "ClientRooms", new { roomId = model.RoomId, clientPin = model.PersonalIdentityNumber });
        }

        [AllowAnonymous]
        public IActionResult Details(int roomId)
        {
            var room = roomsService.GetById<RoomDetailsViewModel>(roomId);

            if (room == null)
            {
                return this.Redirect($"RoomError?roomId={roomId}");
            }

           room.ImagesSrc = roomsService.GetRoomImages(room.Id);

            return this.View(room);
        }

        public IActionResult AddImage(int roomId)
        {
            var model = new AddImageToRoomViewModel { RoomId = roomId };

            return this.View(model);
        }

        [HttpPost]
        public IActionResult AddImage(AddImageToRoomViewModel model)
        {
            var room = roomsService.GetById(model.RoomId);

            if (room == null)
            {
                ModelState.AddModelError(string.Empty, String.Format(GlobalConstants.InvalidRoomErrorMsg,model.RoomId));
                return this.View(model);
            }

            roomsService.AddImageToRoomAsync(model.RoomId, model.Images);

            return this.Redirect($"/Rooms/Details?roomId={model.RoomId}");
        }

        public IActionResult Edit(int roomId)
        {
            var room = roomsService.GetById(roomId);
            var input = AutoMapperConfig.MapperInstance.Map<EditRoomViewModel>(room);
            input.RoomTypes = roomsService.GetAllRoomTypes();
            return this.View(input);
        }

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

        public IActionResult Delete(int roomId)
        {
            roomsService.Delete(roomId);
            return this.Redirect("/Rooms/All");
        }

        public IActionResult Undelete()
        {
            var model = roomsService.GetDeleted<UndeleteViewModel>();
            return this.View(model);
        }

        [HttpPost]
        public IActionResult Undelete(int roomId)
        {
            roomsService.Undelete(roomId);
            return this.Redirect("/Rooms/All");
        }
    }
}
