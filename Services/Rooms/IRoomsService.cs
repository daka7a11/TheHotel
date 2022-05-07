using System.Collections.Generic;
using System.Threading.Tasks;
using TheHotel.Data.Models;
using TheHotel.ViewModels.Rooms;

namespace TheHotel.Services.Rooms
{
    public interface IRoomsService
    {
        int GetCount();

        IEnumerable<KeyValuePair<int, string>> GetAllRoomTypes();

        ICollection<Room> GetAll();
        ICollection<T> GetAll<T>();

        Room GetById(int id);
        T GetById<T>(int id);

        Task AddImageToRoomAsync(int roomId, string imageUrl);

        void Edit(int roomId, EditRoomViewModel model);

        void Delete(int roomId);

        void Undelete(int roomId);

        ICollection<Room> GetDeleted();
        ICollection<T> GetDeleted<T>();
    }
}
