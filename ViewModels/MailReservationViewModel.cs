using AutoMapper;
using System;
using TheHotel.Data.Models;
using TheHotel.Mapping;

namespace TheHotel.ViewModels
{
    public class MailReservationViewModel : IMapFrom<ClientRoom>, IHaveCustomMappings
    {
        public string ClientFirstName { get; set; }

        public string ClientLastName { get; set; }

        public string ClientPersonalIdentityNumber { get; set; }

        public string ClientPhone { get; set; }

        public int RoomId { get; set; }

        public string RoomType { get; set; }

        public int RoomFloor { get; set; }

        public decimal RoomPrice { get; set; }

        public DateTime AccommodationDate { get; set; }

        public DateTime DepartureDate { get; set; }

        public decimal TotalDiscount { get; set; }

        public decimal TotalPrice { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ClientRoom, MailReservationViewModel>()
                .ForMember(x => x.RoomType, y => y.MapFrom(x => x.Room.RoomType.Type));
        }
    }
}
