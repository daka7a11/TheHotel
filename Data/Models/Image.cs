namespace TheHotel.Data.Models
{
    public class Image
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public int RoomId { get; set; }

        public Room Room { get; set; }
    }
}
