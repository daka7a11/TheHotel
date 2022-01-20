using System.Collections.Generic;
using System.Threading.Tasks;
using TheHotel.Data.Models;

namespace TheHotel.Services.ClientRooms
{
    public interface IClientRoomsService
    {
        Task AddAsync(ClientRoom clientRoom);

        ICollection<ClientRoom> GetAll();
    }
}
