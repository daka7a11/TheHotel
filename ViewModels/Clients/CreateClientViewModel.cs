using System.ComponentModel.DataAnnotations;
using TheHotel.Common;

namespace TheHotel.ViewModels.Clients
{
    public class CreateClientViewModel
    {
        [Required]
        [MinLength(GlobalConstants.ClientNameMinLength, ErrorMessage = GlobalConstants.ClientNameErrorMsg)]
        [MaxLength(GlobalConstants.ClientNameMaxLength, ErrorMessage = GlobalConstants.ClientNameErrorMsg)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(GlobalConstants.ClientNameMinLength, ErrorMessage = GlobalConstants.ClientNameErrorMsg)]
        [MaxLength(GlobalConstants.ClientNameMaxLength, ErrorMessage = GlobalConstants.ClientNameErrorMsg)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [MinLength(GlobalConstants.PINMinLegth, ErrorMessage = GlobalConstants.PersonalIdentityNumberErrorMsg)]
        [MaxLength(GlobalConstants.PINMaxLegth, ErrorMessage = GlobalConstants.PersonalIdentityNumberErrorMsg)]
        [Display(Name = "Personal identity number")]
        public string PersonalIdentityNumber { get; set; }

        [Required]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
