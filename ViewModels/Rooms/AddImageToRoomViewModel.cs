using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheHotel.ViewModels.Rooms
{
    public class AddImageToRoomViewModel
    {
        public int RoomId { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }
    }
}
