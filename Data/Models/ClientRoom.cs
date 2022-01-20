using System;

namespace TheHotel.Data.Models
{
    public class ClientRoom
    {
        public int Id { get; set; }

        public DateTime AccommodationDate { get; set; }

        public DateTime DepartureDate { get; set; }

        public int RoomId { get; set; }

        public Room Room { get; set; }

        public string ClientId { get; set; }

        public Client Client { get; set; }
    }
}
