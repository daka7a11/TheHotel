using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TheHotel.Data.Models.Enumerables;

namespace TheHotel.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        public ProductType ProductType { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}
