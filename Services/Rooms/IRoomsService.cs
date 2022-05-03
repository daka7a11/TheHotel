using System.Collections.Generic;
using System.Threading.Tasks;
using TheHotel.Data.Models;

namespace TheHotel.Services.Rooms
{
    public interface IRoomsService
    {
        int GetCount();

        ICollection<Room> GetAll();
        ICollection<T> GetAll<T>();

        Room GetById(int id);
        T GetById<T>(int id);

        Task AddImageToRoomAsync(int roomId, string imageUrl);
    }
}
