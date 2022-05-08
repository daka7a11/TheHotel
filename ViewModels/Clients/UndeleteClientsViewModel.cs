using System;
using TheHotel.Data.Models;
using TheHotel.Mapping;

namespace TheHotel.ViewModels.Clients
{
    public class UndeleteClientsViewModel : IMapFrom<Client>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PersonalIdentityNumber { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public DateTime DeletedOn { get; set; }
    }
}
