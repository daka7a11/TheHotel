using System.ComponentModel.DataAnnotations;
using TheHotel.Common;

namespace TheHotel.ViewModels.Rooms
{
    public class HireRoomViewModel
    {
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Personal Identity Number is required!")]
        [MinLength(GlobalConstants.PINMinLegth, ErrorMessage = GlobalConstants.PersonalIdentityNumberErrorMsg)]
        [MaxLength(GlobalConstants.PINMaxLegth, ErrorMessage = GlobalConstants.PersonalIdentityNumberErrorMsg)]
        [Display(Name = "Personal Identity Number")]
        public string PersonalIdentityNumber { get; set; }
    }
}
