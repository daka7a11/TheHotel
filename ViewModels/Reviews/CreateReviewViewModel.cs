using System.ComponentModel.DataAnnotations;
using TheHotel.Common;
using TheHotel.Data.Models;
using TheHotel.Mapping;

namespace TheHotel.ViewModels.Reviews
{
    public class CreateReviewViewModel : IMapTo<Review>
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
