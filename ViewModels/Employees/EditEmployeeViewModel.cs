using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TheHotel.Common;
using TheHotel.Data.Models;
using TheHotel.Mapping;

namespace TheHotel.ViewModels.Employees
{
    public class EditEmployeeViewModel :IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredNameErrorMsg)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredMiddleNamerrorMsg)]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredLastNameErrorMsg)]
        public string LastName { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredPINErrorMsg)]
        [MinLength(GlobalConstants.PINMinLength, ErrorMessage = GlobalConstants.PersonalIdentityNumberErrorMsg)]
        [MaxLength(GlobalConstants.PINMaxLength, ErrorMessage = GlobalConstants.PersonalIdentityNumberErrorMsg)]
        [RegularExpression(GlobalConstants.PinOnlyDigits, ErrorMessage = GlobalConstants.PersonalIdentityNumberErrorMsg)]
        public string PersonalIdentityNumber { get; set; }

        public string PhoneNumber { get; set; }

        public string RoleId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Roles { get; set; }
    }
}
