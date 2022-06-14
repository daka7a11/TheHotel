namespace TheHotel.ViewModels.Rooms
{
    public class AllRoomsViewModel
    {
        public int Id { get; set; }

        public int Size { get; set; }

        public string RoomType { get; set; }

        public int Floor { get; set; }

        public decimal Price { get; set; }

        public int MaxGuests { get; set; }

        public string Description { get; set; }

        public string FirstImgSrc { get; set; }
    }
}
