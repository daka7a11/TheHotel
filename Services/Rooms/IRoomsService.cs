using System.Collections.Generic;
using TheHotel.Data.Models;

namespace TheHotel.Services.Rooms
{
    public interface IRoomsService
    {
        int GetCount();

        ICollection<Room> GetAll();

        T GetById<T>(int id);
    }
}
