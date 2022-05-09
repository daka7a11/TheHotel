using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheHotel.Data;
using TheHotel.Data.Models;
using TheHotel.Data.Repositories;
using TheHotel.Mapping;
using TheHotel.ViewModels.Rooms;

namespace TheHotel.Services.Rooms
{
    public class RoomsService : IRoomsService
    {
        private readonly IDeletableEntityRepository<Room> roomsRepository;
        private readonly IDeletableEntityRepository<Image> imagesRepository;
        private readonly IDeletableEntityRepository<RoomType> roomTypesRepository;

        public RoomsService(IDeletableEntityRepository<Room> roomsRepository,
            IDeletableEntityRepository<Image> imagesRepository,
            IDeletableEntityRepository<RoomType> roomTypesRepository)
        {
            this.roomsRepository = roomsRepository;
            this.imagesRepository = imagesRepository;
            this.roomTypesRepository = roomTypesRepository;
        }

        public async Task AddImageToRoomAsync(int roomId, string imageUrl)
        {
            await imagesRepository.AddAsync(new Image() { RoomId = roomId, Url = imageUrl });
            await imagesRepository.SaveChangesAsync();
        }

        public void Delete(int roomId)
        {
            var room = roomsRepository.All().FirstOrDefault(x => x.Id == roomId);
            roomsRepository.Delete(room);
            roomsRepository.SaveChanges();
        }

        public void Edit(int roomId, EditRoomViewModel model)
        {
            var room = roomsRepository.All().FirstOrDefault(x => x.Id == roomId);
            room.Size = model.Size;
            room.Price = model.Price;
            room.RoomTypeId = int.Parse(model.RoomTypeId);
            room.Description = model.Description;
            room.ModifiedOn = DateTime.UtcNow;

            roomsRepository.SaveChanges();
        }

        public ICollection<Room> GetAll()
        {
            return roomsRepository.All()
                .Include(x => x.HireDates)
                .Include(x => x.RoomType)
                .Include(x => x.Images)
                .ToList();
        }
        public ICollection<T> GetAll<T>()
        {
            return roomsRepository.All()
                .Include(x => x.HireDates)
                .Include(x => x.RoomType)
                .Include(x => x.Images)
                .To<T>()
                .ToList();
        }

        public IEnumerable<KeyValuePair<int, string>> GetAllRoomTypes()
        {
            return roomTypesRepository.All().Select(x => new KeyValuePair<int, string>(x.Id, x.Type));
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
            return roomsRepository.All()
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
            return roomsRepository.All()
                .Select(x => x.Id)
                .Count();
        }

        public ICollection<Room> GetDeleted()
        {
            return roomsRepository.AllWithDeleted()
                .Where(x => x.IsDeleted == true)
                .ToList();
        }

        public ICollection<T> GetDeleted<T>()
        {
            return roomsRepository.AllWithDeleted()
                .Where(x => x.IsDeleted == true)
                .To<T>()
                .ToList();
        }

        public void Undelete(int roomId)
        {
            var room = roomsRepository.AllWithDeleted().FirstOrDefault(x => x.Id == roomId);
            roomsRepository.Undelete(room);
            roomsRepository.SaveChanges();
        }
    }
}
