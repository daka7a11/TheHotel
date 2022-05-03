using System.ComponentModel.DataAnnotations;

namespace TheHotel.ViewModels.Rooms
{
    public class AddImageToRoomViewModel
    {
        [Display(Name = "Room id")]
        public int RoomId { get; set; }

        [Display(Name = "Image url")]
        public string ImageUrl { get; set; }
    }
}
