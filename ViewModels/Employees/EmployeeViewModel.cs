using AutoMapper;
using System;
using System.Linq;
using TheHotel.Data.Models;
using TheHotel.Mapping;

namespace TheHotel.ViewModels.Employees
{
    public class EmployeeViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PersonalIdentityNumber { get; set; }

        public string Role { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int AcceptedReservation { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUser, EmployeeViewModel>()
                .ForMember(x => x.AcceptedReservation, y => y.MapFrom(x => x.Reservations.Count));
        }
    }
}
