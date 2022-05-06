using Microsoft.AspNetCore.Identity;
using System;

namespace TheHotel.Data.Models
{
    public class ClientRoom : BaseDeletableModel<int>
    {
        public DateTime AccommodationDate { get; set; }

        public DateTime DepartureDate { get; set; }

        public int RoomId { get; set; }

        public Room Room { get; set; }

        public string ClientId { get; set; }

        public Client Client { get; set; }

        public bool IsConfirmed { get; set; } = false;

        public DateTime? RequestDate { get; set; }

        public string EmployeeId { get; set; }

        public ApplicationUser Employee { get; set; }
    }
}
