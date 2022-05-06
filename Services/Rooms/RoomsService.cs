using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheHotel.Data.Models;
using TheHotel.Data.Repositories;
using TheHotel.Mapping;

namespace TheHotel.Services.Rooms
{
    public class RoomsService : IRoomsService
    {
        private readonly IDeletableEntityRepository<Room> roomsRepository;
        private readonly IDeletableEntityRepository<Image> imagesRepository;

        public RoomsService(IDeletableEntityRepository<Room> roomsRepository,
            IDeletableEntityRepository<Image> imagesRepository)
        {
            this.roomsRepository = roomsRepository;
            this.imagesRepository = imagesRepository;
        }

        public async Task AddImageToRoomAsync(int roomId, string imageUrl)
        {
            await imagesRepository.AddAsync(new Image() { RoomId = roomId, Url = imageUrl });
            await imagesRepository.SaveChangesAsync();
        }

        public ICollection<Room> GetAll()
        {
            return roomsRepository.All()
                .Include(x => x.HireDates)
                .Include(x => x.RoomType)
                .Include(x => x.Images)
                .ToList();
        }
        public  ICollection<T> GetAll<T>()
        {
            return roomsRepository.All()
                .Include(x => x.HireDates)
                .Include(x => x.RoomType)
                .Include(x => x.Images)
                .To<T>()
                .ToList();
        }

        public Room GetById(int id)
        {
            return roomsRepository.All()
                .Where(x => x.Id == id)
                .Include(x => x.HireDates)
                .ThenInclude(x => x.Client)
                .Include(x => x.RoomType)
                .Include(x => x.Images)
                .FirstOrDefault();
        }
        public T GetById<T>(int id)
        {
            return  roomsRepository.All()
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
            return  roomsRepository.All()
                .Select(x => x.Id)
                .Count();
        }
    }
}
