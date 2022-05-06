using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TheHotel.Services.Clients;
using TheHotel.ViewModels.Clients;

namespace TheHotel.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ClientsController : Controller
    {
        private readonly IClientsService clientsService;

        public ClientsController(
            IClientsService clientsService)
        {
            this.clientsService = clientsService;
        }

        public IActionResult All()
        {
            var clients = clientsService.GetAll()
                .Select(x => new ClientViewModel()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Phone = x.Phone,
                    Email = x.Email,
                    PersonalIdentityNumber = x.PersonalIdentityNumber,
                    Rooms = x.Rooms,
                });

            return this.View(clients);
        }

        [HttpPost]
        public IActionResult All(string clientPin)
        {
            if (clientPin == null)
            {
                return Redirect("/Clients/All");
            }

            var clients = clientsService.GetAll()
                .Where(x => x.PersonalIdentityNumber == clientPin)
                .Select(x => new ClientViewModel()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Phone = x.Phone,
                    Email = x.Email,
                    PersonalIdentityNumber = x.PersonalIdentityNumber,
                    Rooms = x.Rooms,
                });

            return this.View(clients);
        }

        public IActionResult Details(string clientId)
        {
            var client = clientsService.GetClientById(clientId);

            if (client == null)
            {
                return this.Redirect("Clients/Error");
            }

            var model = new ClientViewModel()
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Phone = client.Phone,
                Email = client.Email,
                PersonalIdentityNumber = client.PersonalIdentityNumber,
                Rooms = client.Rooms,
            };

            return this.View(model);
        }
    }
}
