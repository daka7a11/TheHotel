using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheHotel.Data;
using TheHotel.Data.Models;
using TheHotel.Data.Repositories;
using TheHotel.Mapping;
using TheHotel.ViewModels.Clients;

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

        public void Edit(string clientId, EditClientViewModel model)
        {
            var client = clientsRepository.All().FirstOrDefault(x => x.Id == clientId);
            client.FirstName = model.FirstName;
            client.LastName = model.LastName;
            client.PersonalIdentityNumber = model.PersonalIdentityNumber;
            client.Phone = model.Phone;
            client.Email = model.Email;
            client.ModifiedOn = DateTime.UtcNow;

            clientsRepository.SaveChanges();
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
            return clientsRepository.AllWithDeleted()
                .Include(x => x.Rooms)
                .ThenInclude(x => x.Room)
                .ThenInclude(x => x.RoomType)
                .FirstOrDefault(x => x.Id == clientId);
        }

        public Client GetClientByPIN(string personalIdentityNumber)
        {
            return clientsRepository.AllWithDeleted()
                .FirstOrDefault(x => x.PersonalIdentityNumber == personalIdentityNumber);
        }

        public void Delete(string clientId)
        {
            var client = clientsRepository.All().FirstOrDefault(x => x.Id == clientId);
            clientsRepository.Delete(client);
            clientsRepository.SaveChanges();
        }

        public void HardDelete(string clientId)
        {
            var client = clientsRepository.AllWithDeleted().FirstOrDefault(x => x.Id == clientId);
            clientsRepository.HardDelete(client);
            clientsRepository.SaveChanges();
        }

        public void Undelete(string clientId)
        {
            var client = clientsRepository.AllWithDeleted().FirstOrDefault(x => x.Id == clientId);
            clientsRepository.Undelete(client);
            clientsRepository.SaveChanges();
        }

        public bool IsEmailExist(string email)
        {
            return clientsRepository.All().Any(x => x.Email == email);
        }

        public ICollection<T> GetDeleted<T>()
        {
            return clientsRepository.AllWithDeleted()
                .Where(x => x.IsDeleted == true)
                .To<T>()
                .ToList();
        }
    }
}
