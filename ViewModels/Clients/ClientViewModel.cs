using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TheHotel.Data.Models;
using TheHotel.Mapping;

namespace TheHotel.ViewModels.Clients
{
    public class ClientViewModel : IMapFrom<Client>
    {
        public string Id { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Personal identity number")]
        public string PersonalIdentityNumber { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public virtual ICollection<ClientRoom> Rooms { get; set; }
    }
}
