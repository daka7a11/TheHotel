using System.Linq;
using System.Threading.Tasks;
using TheHotel.Data.Models;

namespace TheHotel.Data.Seeding
{
    public class RoomSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext db)
        {
            if (db.Rooms.Any())
            {
                return;
            }

            var singleRoom = db.RoomTypes.First(x => x.Type == "Single");
            var doubleRoom = db.RoomTypes.First(x => x.Type == "Double");
            var tripleRoom = db.RoomTypes.First(x => x.Type == "Triple");
            var quadRoom = db.RoomTypes.First(x => x.Type == "Quad");

            ////First floor
            await db.Rooms.AddAsync(new Room()
            {
                Size = 30,
                RoomType = singleRoom,
                Price = 50,
                Description = "One single bed, TV, Wi-Fi",
                Floor = 1,
            });
            await db.Rooms.AddAsync(new Room()
            {
                Size = 35,
                RoomType = singleRoom,
                Price = 50,
                Description = "One single bed, TV, Wi-Fi",
                Floor = 1,
            });
            await db.Rooms.AddAsync(new Room()
            {
                Size = 50,
                RoomType = doubleRoom,
                Price = 90,
                Description = "One double bed, TV, Wi-Fi",
                Floor = 1,
            });
            await db.Rooms.AddAsync(new Room()
            {
                Size = 60,
                RoomType = doubleRoom,
                Price = 100,
                Description = "One double bed, TV, Wi-Fi",
                Floor = 1,
            });

            ////Second floor
            await db.Rooms.AddAsync(new Room()
            {
                Size = 30,
                RoomType = singleRoom,
                Price = 50,
                Description = "One single bed, TV, Wi-Fi",
                Floor = 2,
            });
            await db.Rooms.AddAsync(new Room()
            {
                Size = 35,
                RoomType = singleRoom,
                Price = 50,
                Description = "One single bed, TV, Wi-Fi",
                Floor = 2,
            });
            await db.Rooms.AddAsync(new Room()
            {
                Size = 50,
                RoomType = doubleRoom,
                Price = 90,
                Description = "Two single bed, TV, Wi-Fi",
                Floor = 2,
            });
            await db.Rooms.AddAsync(new Room()
            {
                Size = 60,
                RoomType = doubleRoom,
                Price = 100,
                Description = "Two single bed, TV, Wi-Fi",
                Floor = 2,
            });

            ////Third floor
            await db.Rooms.AddAsync(new Room()
            {
                Size = 90,
                RoomType = tripleRoom,
                Price = 140,
                Description = "One room with single bed and one room with double bed, TV, Wi-Fi, Bathtub, Terrace",
                Floor = 3,
            });
            await db.Rooms.AddAsync(new Room()
            {
                Size = 100,
                RoomType = tripleRoom,
                Price = 150,
                Description = "One room with single bed and one room with double bed, TV, Wi-Fi, Bathtub, Terrace",
                Floor = 3,
            });
            await db.Rooms.AddAsync(new Room()
            {
                Size = 120,
                RoomType = quadRoom,
                Price = 220,
                Description = "Two rooms with one double bed, TV, Wi-Fi, Bathtub, Terrace",
                Floor = 3,
            });
            await db.Rooms.AddAsync(new Room()
            {
                Size = 120,
                RoomType = quadRoom,
                Price = 220,
                Description = "Two rooms with one double bed, TV, Wi-Fi, Bathtub, Terrace",
                Floor = 3,
            });

            ////Fourth floor
            await db.Rooms.AddAsync(new Room()
            {
                Size = 100,
                RoomType = tripleRoom,
                Price = 200,
                Description = "One room with single bed and one room with double bed, TV, Wi-Fi, Bathtub, Terrace, Panoramic view",
                Floor = 4,
            });
            await db.Rooms.AddAsync(new Room()
            {
                Size = 100,
                RoomType = tripleRoom,
                Price = 200,
                Description = "One room with single bed and one room with double bed, TV, Wi-Fi, Bathtub, Terrace, Panoramic view",
                Floor = 4,
            });
            await db.Rooms.AddAsync(new Room()
            {
                Size = 120,
                RoomType = quadRoom,
                Price = 250,
                Description = "Two rooms with one double bed, TV, Wi-Fi, Bathtub, Terrace, Panoramic view",
                Floor = 4,
            });
            await db.Rooms.AddAsync(new Room()
            {
                Size = 120,
                RoomType = quadRoom,
                Price = 250,
                Description = "Two rooms with one double bed, TV, Wi-Fi, Bathtub, Terrace, Panoramic view",
                Floor = 4,
            });
        }
    }
}
