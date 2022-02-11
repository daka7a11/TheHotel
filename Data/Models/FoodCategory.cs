using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheHotel.Data.Models
{
    public class FoodCategory
    {
        public FoodCategory()
        {
            FoodItems = new HashSet<FoodItem>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Category { get; set; }

        public virtual ICollection<FoodItem> FoodItems { get; set; }
    }
}
