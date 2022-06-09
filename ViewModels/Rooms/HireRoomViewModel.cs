using System;
using System.ComponentModel.DataAnnotations;
using TheHotel.Common;

namespace TheHotel.ViewModels.Rooms
{
    public class HireRoomViewModel
    {
        public int RoomId { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredPINErrorMsg)]
        [MinLength(GlobalConstants.PINMinLegth,
            ErrorMessage = GlobalConstants.PersonalIdentityNumberErrorMsg)]
        [MaxLength(GlobalConstants.PINMaxLegth, 
            ErrorMessage = GlobalConstants.PersonalIdentityNumberErrorMsg)]
        [RegularExpression("[0-9]*", ErrorMessage = GlobalConstants.PersonalIdentityNumberErrorMsg)]
        public string PersonalIdentityNumber { get; set; }
    }
}
