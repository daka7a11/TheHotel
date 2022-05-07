using System;
using TheHotel.Data.Models;
using TheHotel.Mapping;

namespace TheHotel.ViewModels.Rooms
{
    public class UndeleteViewModel : IMapFrom<Room>
    {
        public int Id { get; set; }

        public int Size { get; set; }

        public int RoomTypeId { get; set; }

        public int Floor { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public DateTime DeletedOn { get; set; }
    }
}
