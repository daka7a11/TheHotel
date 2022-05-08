using System.ComponentModel.DataAnnotations;
using TheHotel.Common;
using TheHotel.Data.Models;
using TheHotel.Mapping;

namespace TheHotel.ViewModels.Clients
{
    public class EditClientViewModel : IMapFrom<Client>
    {
        public string Id { get; set; }

        [Required]
        [MinLength(GlobalConstants.ClientNameMinLength)]
        [MaxLength(GlobalConstants.ClientNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(GlobalConstants.ClientNameMinLength)]
        [MaxLength(GlobalConstants.ClientNameMaxLength)]
        public string LastName { get; set; }

        [Required]
        [MinLength(GlobalConstants.PINMinLegth)]
        [MaxLength(GlobalConstants.PINMaxLegth)]
        public string PersonalIdentityNumber { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
