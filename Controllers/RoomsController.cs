using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TheHotel.Data.Models;
using TheHotel.Services.ClientRooms;
using TheHotel.Services.Rooms;
using TheHotel.ViewModels.Rooms;

namespace TheHotel.Controllers
{
    public class RoomsController : Controller
    {
        private readonly IRoomsService roomsService;
        private readonly IClientRoomsService clientRoomsService;

        public RoomsController(IRoomsService roomsService, IClientRoomsService clientRoomsService)
        {
            this.roomsService = roomsService;
            this.clientRoomsService = clientRoomsService;
        }

        public IActionResult All()
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
                });

            var model = new AllViewModel()
            {
                AllRoomsViewModel = allRooms,
            };

            return this.View(model);
        }

        [HttpPost]
        public IActionResult All(DateTime? accommodationDate, DateTime? departureDate)
        {
            if (accommodationDate == null || departureDate == null )
            {
                return Redirect("/Rooms/All");
            }

            var rooms = roomsService.GetAll()
                .Where(x => IsRoomAvailable(x,accommodationDate,departureDate))
                .Select(x => new AllRoomsViewModel()
                {
                    Id = x.Id,
                    Size = x.Size,
                    RoomType = x.RoomType.Type,
                    Floor = x.Floor,
                    Price = x.Price,
                    Description = x.Description,
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

            return this.RedirectToAction("Tenancy", "Clients", new { roomId = model.RoomId, clientPin = model.PersonalIdentityNumber });
        }

        public IActionResult Details(int roomId)
        {
            var room = roomsService.GetById(roomId);

            if (room == null)
            {
                return this.Redirect($"RoomError?roomId={roomId}");
            }

            var model = new RoomDetailsViewModel()
            {
                Id = room.Id,
                Floor = room.Floor,
                Description = room.Description,
                RoomType = room.RoomType.Type,
                Size = room.Size,
                Price = room.Price,
                HireDates = room.HireDates,
                Images = room.Images,
            };

            return this.View(model);
        }

        private bool IsRoomAvailable(Room room, DateTime? accDate, DateTime? depDate)
        {
            foreach (var hireDate in room.HireDates)
            {
                if (accDate < hireDate.AccommodationDate.Date && depDate > hireDate.AccommodationDate.Date)
                {
                    return false;
                }

                if (accDate >= hireDate.AccommodationDate.Date && accDate.Value.Date < hireDate.DepartureDate.Date)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
