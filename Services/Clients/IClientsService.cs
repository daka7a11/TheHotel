using System.Threading.Tasks;
using TheHotel.Data.Models;

namespace TheHotel.Services.Clients
{
    public interface IClientsService
    {
        Client GetClientByPIN(string personalIdentityNumber);

        Task AddAsync(Data.Models.Client client);
    }
}
