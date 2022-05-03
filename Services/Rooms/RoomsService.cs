using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheHotel.Data;
using TheHotel.Data.Models;
using TheHotel.Mapping;

namespace TheHotel.Services.Rooms
{
    public class RoomsService : IRoomsService
    {
        private readonly ApplicationDbContext db;

        public RoomsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task AddImageToRoomAsync(int roomId, string imageUrl)
        {
            await db.Images.AddAsync(new Image() { RoomId = roomId, Url = imageUrl });
            await db.SaveChangesAsync();
        }

        public ICollection<Room> GetAll()
        {
            return db.Rooms
                .Include(x => x.HireDates)
                .Include(x => x.RoomType)
                .Include(x => x.Images)
                .ToList();
        }
        public ICollection<T> GetAll<T>()
        {
            return db.Rooms
                .Include(x => x.HireDates)
                .Include(x => x.RoomType)
                .Include(x => x.Images)
                .To<T>()
                .ToList();
        }

        public Room GetById(int id)
        {
            return db.Rooms
                .Where(x => x.Id == id)
                .Include(x => x.HireDates)
                .ThenInclude(x => x.Client)
                .Include(x => x.RoomType)
                .Include(x => x.Images)
                .FirstOrDefault();
        }
        public T GetById<T>(int id)
        {
            return db.Rooms
                .Where(x => x.Id == id)
                .Include(x => x.HireDates)
                .ThenInclude(x => x.Client)
                .Include(x => x.RoomType)
                .Include(x => x.Images)
                .To<T>()
                .FirstOrDefault();
        }

        public int GetCount()
        {
            return db.Rooms
                .Select(x => x.Id)
                .Count();
        }
    }
}
