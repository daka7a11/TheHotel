using System.Threading.Tasks;

namespace TheHotel.Data.Seeding
{
    public interface ISeeder
    {
        Task SeedAsync(ApplicationDbContext db);
    }
}
