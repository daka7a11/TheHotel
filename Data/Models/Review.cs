using System.ComponentModel.DataAnnotations;
using TheHotel.Common;

namespace TheHotel.Data.Models
{
    public class Review : BaseDeletableModel<int>
    {
        [Required]
        [MinLength(GlobalConstants.ClientNameMinLength)]
        [MaxLength(GlobalConstants.ClientNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(GlobalConstants.ClientNameMinLength)]
        [MaxLength(GlobalConstants.ClientNameMaxLength)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public int Rating { get; set; }

        [Required]
        [MinLength(GlobalConstants.MessageTextMinLength, ErrorMessage = GlobalConstants.MessageLengthErrorMsg)]
        public string Text { get; set; }
    }
}
