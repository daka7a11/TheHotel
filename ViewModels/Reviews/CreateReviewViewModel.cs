using System.ComponentModel.DataAnnotations;
using TheHotel.Common;
using TheHotel.Data.Models;
using TheHotel.Mapping;

namespace TheHotel.ViewModels.Reviews
{
    public class CreateReviewViewModel : IMapTo<Review>
    {
        [Required(ErrorMessage = GlobalConstants.RequiredNameErrorMsg)]
        [MinLength(GlobalConstants.ClientNameMinLength)]
        [MaxLength(GlobalConstants.ClientNameMaxLength)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredLastNameErrorMsg)]
        [MinLength(GlobalConstants.ClientNameMinLength)]
        [MaxLength(GlobalConstants.ClientNameMaxLength)]
        public string LastName { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredEmailErrorMsg)]
        [EmailAddress]
        public string Email { get; set; }

        public int Rating { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredReviewErrorMsg)]
        [MinLength(GlobalConstants.MessageTextMinLength, ErrorMessage = GlobalConstants.MessageLengthErrorMsg)]
        public string Text { get; set; }
    }
}
