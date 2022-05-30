using System.ComponentModel.DataAnnotations;
using TheHotel.Common;

namespace TheHotel.Data.Models
{
    public class Question : BaseDeletableModel<int>
    {
        [Required]
        [MinLength(GlobalConstants.ClientNameMinLength)]
        [MaxLength(GlobalConstants.ClientNameMaxLength)]
        public string FirstName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        [MinLength(GlobalConstants.TitleMinLength, ErrorMessage = GlobalConstants.TitleLengthErrorMsg)]
        public string Title { get; set; }

        [Required]
        [MinLength(GlobalConstants.MessageTextMinLength, ErrorMessage = GlobalConstants.MessageLengthErrorMsg)]
        public string Text { get; set; }
    }
}
