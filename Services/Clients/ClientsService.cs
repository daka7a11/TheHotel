using System.Linq;
using System.Threading.Tasks;
using TheHotel.Data;
using TheHotel.Data.Models;

namespace TheHotel.Services.Clients
{
    public class ClientsService : IClientsService
    {
        private readonly ApplicationDbContext db;

        public ClientsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task AddAsync(Client client)
        {
            await db.Clients.AddAsync(client);
            await db.SaveChangesAsync();
        }

        public Client GetClientByPIN(string personalIdentityNumber)
        {
            return db.Clients.FirstOrDefault(x => x.PersonalIdentityNumber == personalIdentityNumber);
        }
    }
}
