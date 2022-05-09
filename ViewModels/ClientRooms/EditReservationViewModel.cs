using System;
using TheHotel.Data.Models;
using TheHotel.Mapping;

namespace TheHotel.ViewModels.ClientRooms
{
    public class EditReservationViewModel : IMapFrom<ClientRoom>
    {
        public int Id { get; set; }

        public DateTime AccommodationDate { get; set; }

        public DateTime DepartureDate { get; set; }
    }
}
