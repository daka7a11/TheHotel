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

            await db.RoomTypes.AddRangeAsync(new RoomType() { Type = "Единична" , MaxGuests = 1}, new RoomType() { Type = "Двойна", MaxGuests = 2 }, new RoomType() { Type = "Тройна", MaxGuests = 3 }, new RoomType() { Type = "Апартамент", MaxGuests = 4 });
        }
    }
}
