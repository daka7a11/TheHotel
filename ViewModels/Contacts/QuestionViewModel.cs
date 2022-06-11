using System.ComponentModel.DataAnnotations;
using TheHotel.Common;
using TheHotel.Data.Models;
using TheHotel.Mapping;

namespace TheHotel.ViewModels.Contacts
{
    public class QuestionViewModel : IMapTo<Question>
    {
        [Required(ErrorMessage = GlobalConstants.RequiredNameErrorMsg)]
        [MinLength(GlobalConstants.ClientNameMinLength)]
        [MaxLength(GlobalConstants.ClientNameMaxLength)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredEmailErrorMsg)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredPhoneErrorMsg)]
        public string Phone { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredTitleErrorMsg)]
        [MinLength(GlobalConstants.TitleMinLength, ErrorMessage = GlobalConstants.TitleLengthErrorMsg)]
        public string Title { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredQuesitonErrorMsg)]
        [MinLength(GlobalConstants.MessageTextMinLength, ErrorMessage = GlobalConstants.MessageLengthErrorMsg)]
        public string Text { get; set; }
    }
}
