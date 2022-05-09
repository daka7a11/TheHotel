using AutoMapper;
using System;
using TheHotel.Data.Models;
using TheHotel.Mapping;

namespace TheHotel.ViewModels.ClientRooms
{
    public class ReservationDetailsViewModel : IMapFrom<ClientRoom>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string ClientFirstName { get; set; }

        public string ClientLastName { get; set; }

        public string ClientPersonalIdentityNumber { get; set; }

        public string ClientPhone { get; set; }

        public string ClientEmail { get; set; }

        public int RoomId { get; set; }

        public string RoomType { get; set; }

        public int RoomSize { get; set; }

        public int RoomFloor { get; set; }

        public decimal RoomPrice { get; set; }

        public string RoomDescription { get; set; }

        public DateTime AccommodationDate { get; set; }

        public DateTime DepartureDate { get; set; }

        public bool IsConfirmed { get; set; }

        public bool IsDeleted { get; set; }

        public string EmployeeNames { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ClientRoom, ReservationDetailsViewModel>()
                .ForMember(x => x.EmployeeNames, y => y.MapFrom(x => $"{x.Employee.FirstName} {x.Employee.LastName}"))
                .ForMember(x => x.RoomType, y => y.MapFrom(x => x.Room.RoomType.Type));
        }
    }
}
