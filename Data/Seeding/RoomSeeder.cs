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

            var singleRoom = db.RoomTypes.First(x => x.Type == "Единична");
            var doubleRoom = db.RoomTypes.First(x => x.Type == "Двойна");
            var tripleRoom = db.RoomTypes.First(x => x.Type == "Тройна");
            var apartment = db.RoomTypes.First(x => x.Type == "Апартамент");

            ////First floor
            await db.Rooms.AddAsync(new Room()
            {
                Size = 20,
                RoomType = singleRoom,
                Price = 30,
                Description = "Единично легло, телевизор, климатик и нощно шкафче",
                Floor = 1,
            });
            await db.Rooms.AddAsync(new Room()
            {
                Size = 20,
                RoomType = singleRoom,
                Price = 30,
                Description = "Единично легло, телевизор, климатик и нощно шкафче",
                Floor = 1,
            });
            await db.Rooms.AddAsync(new Room()
            {
                Size = 50,
                RoomType = doubleRoom,
                Price = 60,
                Description = "Двойно легло, телевизор, климатик, закачалка и гардероб",
                Floor = 1,
            });
            await db.Rooms.AddAsync(new Room()
            {
                Size = 50,
                RoomType = doubleRoom,
                Price = 70,
                Description = "Двойно легло, тераса с масичка, телевизор, климатик, закачалка и гардероб",
                Floor = 1,
            });

            ////Second floor
            await db.Rooms.AddAsync(new Room()
            {
                Size = 30,
                RoomType = singleRoom,
                Price = 40,
                Description = "Единично легло, тераса с масичка, телевизор, климатик и нощно шкафче",
                Floor = 2,
            });
            await db.Rooms.AddAsync(new Room()
            {
                Size = 30,
                RoomType = singleRoom,
                Price = 40,
                Description = "Единично легло, тераса с масичка, телевизор, климатик и нощно шкафче",
                Floor = 2,
            });
            await db.Rooms.AddAsync(new Room()
            {
                Size = 50,
                RoomType = doubleRoom,
                Price = 80,
                Description = "Двойно легло, тераса с масичка, телевизор, климатик, закачалка и гардероб",
                Floor = 2,
            });
            await db.Rooms.AddAsync(new Room()
            {
                Size = 50,
                RoomType = doubleRoom,
                Price = 80,
                Description = "Двойно легло, тераса с масичка, телевизор, климатик, закачалка и гардероб",
                Floor = 2,
            });

            ////Third floor
            await db.Rooms.AddAsync(new Room()
            {
                Size = 70,
                RoomType = tripleRoom,
                Price = 120,
                Description = "Двойно легло, единично легло, тераса с масичка, телевизор, вана, гардероб и климатик",
                Floor = 3,
            });
            await db.Rooms.AddAsync(new Room()
            {
                Size = 70,
                RoomType = tripleRoom,
                Price = 120,
                Description = "Двойно легло, единично легло, тераса с масичка, телевизор, вана, гардероб и климатик",
                Floor = 3,
            });
            await db.Rooms.AddAsync(new Room()
            {
                Size = 120,
                RoomType = apartment,
                Price = 180,
                Description = "Две отделни стаи с двойни легла, кухненски бокс, тераса, телевизор, хладилник, фризер, климатик, гардероб и вана",
                Floor = 3,
            });
            await db.Rooms.AddAsync(new Room()
            {
                Size = 120,
                RoomType = apartment,
                Price = 180,
                Description = "Две отделни стаи с двойни легла, кухненски бокс, тераса, телевизор, хладилник, фризер, климатик, гардероб и вана",
                Floor = 3,
            });

            ////Fourth floor
            await db.Rooms.AddAsync(new Room()
            {
                Size = 100,
                RoomType = tripleRoom,
                Price = 140,
                Description = "Двойно легло, единично легло, панорамна гледка, тераса с масичка, телевизор, вана, гардероб и климатик",
                Floor = 4,
            });
            await db.Rooms.AddAsync(new Room()
            {
                Size = 100,
                RoomType = tripleRoom,
                Price = 140,
                Description = "Двойно легло, единично легло, панорамна гледка, тераса с масичка, телевизор, вана, гардероб и климатик",
                Floor = 4,
            });
            await db.Rooms.AddAsync(new Room()
            {
                Size = 120,
                RoomType = apartment,
                Price = 200,
                Description = "Две отделни стаи с двойни легла, панорамна гледка, кухненски бокс, тераса, телевизор, хладилник, фризер, климатик, гардероб, вана",
                Floor = 4,
            });
            await db.Rooms.AddAsync(new Room()
            {
                Size = 120,
                RoomType = apartment,
                Price = 200,
                Description = "Две отделни стаи с двойни легла, панорамна гледка, кухненски бокс, тераса, телевизор, хладилник, фризер, климатик, гардероб, вана",
                Floor = 4,
            });
        }
    }
}
