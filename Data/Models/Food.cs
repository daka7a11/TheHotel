using System.ComponentModel.DataAnnotations.Schema;
using TheHotel.Data.Models.Enumerables;

namespace TheHotel.Data.Models
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public FoodType FoodType { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}
