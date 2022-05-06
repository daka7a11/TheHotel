using System.Collections.Generic;
using System.Threading.Tasks;
using TheHotel.Data.Models;

namespace TheHotel.Services.Clients
{
    public interface IClientsService
    {
        Client GetClientByPIN(string personalIdentityNumber);

        Client GetClientById(string clientId);

        Task AddAsync(Client client);

        IEnumerable<Client> GetAll();
        IEnumerable<T> GetAll<T>();

        bool IsEmailExist(string email);
    }
}
