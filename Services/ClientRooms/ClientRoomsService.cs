using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheHotel.Data;
using TheHotel.Data.Models;

namespace TheHotel.Services.ClientRooms
{
    public class ClientRoomsService : IClientRoomsService
    {
        private readonly ApplicationDbContext db;

        public ClientRoomsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task AddAsync(ClientRoom clientRoom)
        {
            await db.ClientRooms.AddAsync(clientRoom);
            await db.SaveChangesAsync();
        }

        public ICollection<ClientRoom> GetAll()
        {
            return db.ClientRooms.ToList();
        }
    }
}
