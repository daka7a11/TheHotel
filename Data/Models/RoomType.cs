using System.Collections.Generic;

namespace TheHotel.Data.Models
{
    public class RoomType : BaseDeletableModel<int>
    {
        public RoomType()
        {
            this.Rooms = new HashSet<Room>();
        }
        public string Type { get; set; }

        public int MaxGuests { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
