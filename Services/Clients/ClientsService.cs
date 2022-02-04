﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public IEnumerable<Client> GetAll()
        {
            return db.Clients
                .Include(x => x.Rooms)
                .ThenInclude(x => x.Room)
                .ToList();
        }

        public Client GetClientById(string clientId)
        {
            return db.Clients
                .Include(x => x.Rooms)
                .ThenInclude(x => x.Room)
                .ThenInclude(x => x.RoomType)
                .FirstOrDefault(x => x.Id == clientId);
        }

        public Client GetClientByPIN(string personalIdentityNumber)
        {
            return db.Clients.FirstOrDefault(x => x.PersonalIdentityNumber == personalIdentityNumber);
        }
    }
}
