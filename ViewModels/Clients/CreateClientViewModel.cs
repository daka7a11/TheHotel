using System.ComponentModel.DataAnnotations;
using TheHotel.Common;
using TheHotel.Data.Models;
using TheHotel.Mapping;

namespace TheHotel.ViewModels.Clients
{
    public class CreateClientViewModel : IMapFrom<Client>
    {


        [Required]
        [MinLength(GlobalConstants.ClientNameMinLength, ErrorMessage = GlobalConstants.ClientNameLengthErrorMsg)]
        [MaxLength(GlobalConstants.ClientNameMaxLength, ErrorMessage = GlobalConstants.ClientNameLengthErrorMsg)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(GlobalConstants.ClientNameMinLength, ErrorMessage = GlobalConstants.ClientNameLengthErrorMsg)]
        [MaxLength(GlobalConstants.ClientNameMaxLength, ErrorMessage = GlobalConstants.ClientNameLengthErrorMsg)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [MinLength(GlobalConstants.PINMinLength, ErrorMessage = GlobalConstants.PersonalIdentityNumberErrorMsg)]
        [MaxLength(GlobalConstants.PINMaxLength, ErrorMessage = GlobalConstants.PersonalIdentityNumberErrorMsg)]
        [Display(Name = "Personal identity number")]
        public string PersonalIdentityNumber { get; set; }

        [Required]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
