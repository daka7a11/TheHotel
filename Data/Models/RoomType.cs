using System.Collections.Generic;

namespace TheHotel.Data.Models
{
    public class RoomType
    {
        public RoomType()
        {
            this.Rooms = new HashSet<Room>();
        }
        public int Id { get; set; }

        public string Type { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
