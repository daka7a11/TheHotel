using System.Collections.Generic;
using TheHotel.Data.Models;

namespace TheHotel.ViewModels.Rooms
{
    public class RoomDetailsViewModel
    {
        public int Id { get; set; }

        public int Size { get; set; }

        public string RoomType { get; set; }

        public int Floor { get; set; }

        public decimal Price { get; set; }

        public ICollection<ClientRoom> HireDates { get; set; }

        public ICollection<Image> Images { get; set; }

        public string Description { get; set; }
    }
}
