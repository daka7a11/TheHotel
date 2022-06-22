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

        [AllowAnonymous]
        [Authorize(Roles = GlobalConstants.ReceptionistRole)]
        public IActionResult All()
        {
            var clients = clientsService.GetAll<ClientViewModel>();

            return this.View(clients);
        }

        [AllowAnonymous]
        [Authorize(Roles = GlobalConstants.ReceptionistRole)]
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

        [AllowAnonymous]
        [Authorize(Roles = GlobalConstants.ReceptionistRole)]
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

        [AllowAnonymous]
        [Authorize(Roles = GlobalConstants.ReceptionistRole)]
        public IActionResult Edit(string clientId)
        {
            var model = AutoMapperConfig.MapperInstance.Map<EditClientViewModel>(clientsService.GetClientById(clientId));
            return this.View(model);
        }

        [AllowAnonymous]
        [Authorize(Roles = GlobalConstants.ReceptionistRole)]
        [HttpPost]
        public IActionResult Edit(EditClientViewModel model)
        {
            clientsService.Edit(model.Id, model);

            return this.Redirect($"/Clients/Details?clientId={model.Id}");
        }

        public IActionResult Delete(string clientId)
        {
            clientsService.Delete(clientId);

            return this.Redirect($"/Clients/Details?clientId={clientId}");
        }

        public IActionResult Undelete()
        {
            var model = clientsService.GetDeleted<UndeleteClientsViewModel>();

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Undelete(string clientId)
        {
            clientsService.Undelete(clientId);

            return this.Redirect($"/Clients/Details?clientId={clientId}");
        }

        [HttpPost]
        public IActionResult HardDelete(string clientId)
        {
            clientsService.HardDelete(clientId);

            return this.RedirectToAction(nameof(All));
        }
    }
}
