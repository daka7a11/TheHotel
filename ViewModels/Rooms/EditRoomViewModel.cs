using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TheHotel.Common;
using TheHotel.Data.Models;
using TheHotel.Mapping;

namespace TheHotel.ViewModels.Rooms
{
    public class EditRoomViewModel : IMapFrom<Room>
    {
        public int Id { get; set; }

        public int Size { get; set; }

        public string RoomTypeId { get; set; }

        public IEnumerable<KeyValuePair<int,string>> RoomTypes { get; set; }

        [Range(GlobalConstants.RoomMinPrice, GlobalConstants.RoomMaxPrice)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
