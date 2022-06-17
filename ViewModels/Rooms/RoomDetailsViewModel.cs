using AutoMapper;
using System;
using System.Collections.Generic;
using TheHotel.Data.Models;
using TheHotel.Mapping;

namespace TheHotel.ViewModels.Rooms
{
    public class RoomDetailsViewModel : IMapFrom<Room>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public int Size { get; set; }

        public string RoomType { get; set; }

        public int Floor { get; set; }

        public decimal Price { get; set; }

        public ICollection<ClientRoom> HireDates { get; set; }

        public IEnumerable<string> ImagesSrc { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string Description { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Room, RoomDetailsViewModel>()
                .ForMember(x => x.RoomType, opt =>
                    opt.MapFrom(x => x.RoomType.Type));
        }
    }
}
