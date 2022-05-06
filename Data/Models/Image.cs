namespace TheHotel.Data.Models
{
    public class Image : BaseDeletableModel<int>
    {
        public string Url { get; set; }

        public int RoomId { get; set; }

        public Room Room { get; set; }
    }
}
