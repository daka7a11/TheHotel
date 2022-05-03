using AutoMapper;
using System;
using TheHotel.Common;
using TheHotel.Data.Models;
using TheHotel.Mapping;
using TheHotel.Services.Rooms;

namespace TheHotel.ViewModels.ClientRooms
{
    public class RequestDetailsViewModel : IMapFrom<ClientRoom>
    {
        public int RoomId { get; set; }

        public string ClientFirstName { get; set; }

        public string ClientLastName { get; set; }

        public string ClientPersonalIdentityNumber { get; set; }

        public string ClientPhone { get; set; }

        public string ClientEmail { get; set; }

        public DateTime AccommodationDate { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime? RequestDate { get; set; }

        public bool IsStillAvailable { get; set; }
    }
}
