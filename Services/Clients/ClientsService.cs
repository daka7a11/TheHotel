using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheHotel.Data;
using TheHotel.Data.Models;
using TheHotel.Data.Repositories;
using TheHotel.Mapping;

namespace TheHotel.Services.Clients
{
    public class ClientsService : IClientsService
    {
        private readonly IDeletableEntityRepository<Client> clientsRepository;

        public ClientsService(IDeletableEntityRepository<Client> clientsRepository)
        {
            this.clientsRepository = clientsRepository;
        }

        public async Task AddAsync(Client client)
        {
            await clientsRepository.AddAsync(client);
            await clientsRepository.SaveChangesAsync();
        }

        public IEnumerable<Client> GetAll()
        {
            return clientsRepository.All()
                .Include(x => x.Rooms)
                .ThenInclude(x => x.Room)
                .ToList();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return clientsRepository.All()
                .Include(x => x.Rooms)
                .ThenInclude(x => x.Room)
                .To<T>()
                .ToList();
        }

        public Client GetClientById(string clientId)
        {
            return clientsRepository.All()
                .Include(x => x.Rooms)
                .ThenInclude(x => x.Room)
                .ThenInclude(x => x.RoomType)
                .FirstOrDefault(x => x.Id == clientId);
        }

        public Client GetClientByPIN(string personalIdentityNumber)
        {
            return clientsRepository.All()
                .FirstOrDefault(x => x.PersonalIdentityNumber == personalIdentityNumber);
        }

        public bool IsEmailExist(string email)
        {
            return clientsRepository.All().Any(x => x.Email == email);
        }
    }
}
