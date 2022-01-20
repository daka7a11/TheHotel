using System.Linq;
using System.Threading.Tasks;
using TheHotel.Data.Models;

namespace TheHotel.Data.Seeding
{
    public class RoomTypeSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext db)
        {
            if (db.RoomTypes.Any())
            {
                return;
            }

            await db.RoomTypes.AddRangeAsync(new RoomType() { Type = "Single" }, new RoomType() { Type = "Double" }, new RoomType() { Type = "Triple" }, new RoomType() { Type = "Quad" });
        }
    }
}
