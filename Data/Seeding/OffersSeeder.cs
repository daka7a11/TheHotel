using System;
using System.Linq;
using System.Threading.Tasks;
using TheHotel.Data.Models;

namespace TheHotel.Data.Seeding
{
    public class OffersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext db)
        {
            if (!db.Offers.Any())
            {
                await db.Offers.AddAsync(new Offer() 
                { 
                    Name = "Отстъпка при отсядане за повече от 5 нощувки",
                    Discount = 10, StartDate = DateTime.UtcNow,
                    Description = "Ако вашата резервация е за повече от 5 нощувки, вие получавате отстъпка.",
                    CreatedOn = DateTime.UtcNow,
                    ImageUrl = "https://img.freepik.com/free-photo/10-percent-off-promotion_2227-142.jpg",
                });
                await db.SaveChangesAsync();
            }
        }
    }
}
