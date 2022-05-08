using System.Collections.Generic;
using System.Threading.Tasks;
using TheHotel.Data.Models;
using TheHotel.ViewModels.Clients;

namespace TheHotel.Services.Clients
{
    public interface IClientsService
    {
        Client GetClientByPIN(string personalIdentityNumber);

        Client GetClientById(string clientId);

        Task AddAsync(Client client);

        IEnumerable<Client> GetAll();
        IEnumerable<T> GetAll<T>();

        void Edit(string clientId, EditClientViewModel model);

        void Delete(string clientId);

        void HardDelete(string clientId);

        void Undelete(string clientId);

        ICollection<T> GetDeleted<T>();

        bool IsEmailExist(string email);
    }
}
