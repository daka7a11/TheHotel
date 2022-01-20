using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TheHotel.Common;

namespace TheHotel.ViewModels.Clients
{
    public class TenancyViewModel : IValidatableObject
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

        [Display(Name = "Room id")]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Accommodation date is required!")]
        [DataType(DataType.Date)]
        [Display(Name = "Accommodation date")]
        public DateTime? AccommodationDate { get; set; }

        [Required(ErrorMessage = "Departure date is required!")]
        [DataType(DataType.Date)]
        [Display(Name = "Departure date")]
        public DateTime? DepartureDate { get; set; }

        [Display(Name = "Update client information")]
        public bool UpdateClientInfo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.DepartureDate <= this.AccommodationDate)
            {
                yield return new ValidationResult("Departure date cannot be less or equal than accommodation date!");
            }
        }
    }
}
