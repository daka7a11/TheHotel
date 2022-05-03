using System;
using TheHotel.Data.Models;
using TheHotel.Mapping;

namespace TheHotel.ViewModels.Rooms
{
    public class RoomRequestViewModel : IMapFrom<ClientRoom>
    {
        public int Id { get; set; }

        public string ClientFirstName { get; set; }

        public string ClientLastName { get; set; }

        public string ClientPersonalIdentityNumber { get; set; }

        public string ClientPhone { get; set; }

        public string ClientEmail { get; set; }

        public int RoomId { get; set; }

        public DateTime AccommodationDate { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime? RequestDate { get; set; }

    }
}
