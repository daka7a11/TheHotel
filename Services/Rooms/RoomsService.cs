using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public ICollection<Room> GetAll()
        {
            return db.Rooms
                .Include(x => x.HireDates)
                .Include(x => x.RoomType)
                .Include(x => x.Images)
                .ToList();
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
