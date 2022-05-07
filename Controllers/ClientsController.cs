using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TheHotel.Common;
using TheHotel.Mapping;
using TheHotel.Services.Clients;
using TheHotel.ViewModels.Clients;

namespace TheHotel.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
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
            var clients = clientsService.GetAll<ClientViewModel>();

            return this.View(clients);
        }

        [HttpPost]
        public IActionResult All(string clientPin)
        {
            if (clientPin == null)
            {
                return Redirect("/Clients/All");
            }

            var clients = clientsService.GetAll<ClientViewModel>()
                .Where(x => x.PersonalIdentityNumber == clientPin);

            return this.View(clients);
        }

        public IActionResult Details(string clientId)
        {
            var client = clientsService.GetClientById(clientId);

            if (client == null)
            {
                return this.Redirect("Clients/Error");
            }

            var model = AutoMapperConfig.MapperInstance.Map<ClientViewModel>(client);

            return this.View(model);
        }
    }
}
