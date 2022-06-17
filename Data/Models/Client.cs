using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TheHotel.Common;

namespace TheHotel.Data.Models
{
    public class Client : BaseDeletableModel<string>
    {
        public Client()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Rooms = new HashSet<ClientRoom>();
        }
        [Required]
        [MinLength(GlobalConstants.ClientNameMinLength)]
        [MaxLength(GlobalConstants.ClientNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(GlobalConstants.ClientNameMinLength)]
        [MaxLength(GlobalConstants.ClientNameMaxLength)]
        public string LastName { get; set; }

        [Required]
        [MinLength(GlobalConstants.PINMinLength)]
        [MaxLength(GlobalConstants.PINMaxLength)]
        public string PersonalIdentityNumber { get; set; }

        [Required]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public virtual ICollection<ClientRoom> Rooms { get; set; }
    }
}
