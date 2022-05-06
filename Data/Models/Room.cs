using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TheHotel.Common;
using TheHotel.Mapping;

namespace TheHotel.Data.Models
{
    public class Room : BaseDeletableModel<int>, IMapFrom<Room>
    {
        public Room()
        {
            this.HireDates = new HashSet<ClientRoom>();
        }
        [Required]
        [Range(GlobalConstants.RoomMinSize, GlobalConstants.RoomMaxSize)]
        public int Size { get; set; }

        public int RoomTypeId { get; set; }

        public RoomType RoomType { get; set; }

        [Range(GlobalConstants.RoomMinFloor, GlobalConstants.RoomMaxFloor)]
        public int Floor { get; set; }

        [Range(GlobalConstants.RoomMinPrice, GlobalConstants.RoomMaxPrice)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public ICollection<ClientRoom> HireDates { get; set; }

        public ICollection<Image> Images { get; set; }

        public string Description { get; set; }
    }
}
