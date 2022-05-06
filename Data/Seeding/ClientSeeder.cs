using System.Linq;
using System.Threading.Tasks;
using TheHotel.Data.Models;

namespace TheHotel.Data.Seeding
{
    public class ClientSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext db)
        {
            if (db.Clients.Any())
            {
                return;
            }

            await db.Clients.AddAsync(new Client()
            {
                FirstName = "Клиент",
                LastName = "Клиент",
                Email = "client@abv.bg",
                PersonalIdentityNumber = "1111111111",
                Phone = "0000000000",
            });
        }
    }
}
