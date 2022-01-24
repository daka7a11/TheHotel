using System.Collections.Generic;
using System.Threading.Tasks;
using TheHotel.Data.Models;

namespace TheHotel.Services.Clients
{
    public interface IClientsService
    {
        Client GetClientByPIN(string personalIdentityNumber);

        Client GetClientById(string clientId);

        Task AddAsync(Data.Models.Client client);

        IEnumerable<Client> GetAll();
    }
}
