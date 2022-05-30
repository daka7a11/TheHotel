using System;
using TheHotel.Data.Models;
using TheHotel.Mapping;

namespace TheHotel.ViewModels.ClientRooms
{
    public class DeletedClientRoomsViewModel : IMapFrom<ClientRoom>
    {
        public int Id { get; set; }

        public string ClientFirstName { get; set; }

        public string ClientLastName { get; set; }

        public int RoomId { get; set; }

        public DateTime AccommodationDate { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime? RequestDate { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
