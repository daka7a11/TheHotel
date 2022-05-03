using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheHotel.Data.Seeding
{
    public class ApplicationDbContextSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            var seeders = new List<ISeeder>
                          {
                              new RoomTypeSeeder(),
                              new RoomSeeder(),
                              new ClientSeeder(),
                              new ImageSeeder(),
                          };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
