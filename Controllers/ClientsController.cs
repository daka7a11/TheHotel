using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheHotel.Common;
using TheHotel.Data;
using TheHotel.Data.Models;
using TheHotel.Services.ClientRooms;
using TheHotel.Services.Clients;
using TheHotel.Services.Rooms;
using TheHotel.ViewModels.Clients;

namespace TheHotel.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientsService clientsService;
        private readonly IClientRoomsService clientRoomsService;
        private readonly IRoomsService roomsService;
        private readonly ApplicationDbContext db;

        public ClientsController(
            IClientsService clientsService,
            IClientRoomsService clientRoomsService,
            IRoomsService roomsService,
            ApplicationDbContext db)
        {
            this.clientsService = clientsService;
            this.clientRoomsService = clientRoomsService;
            this.roomsService = roomsService;
            this.db = db;
        }

        [HttpGet]
        public IActionResult Tenancy(int roomId, string clientPin)
        {
            var client = this.clientsService.GetClientByPIN(clientPin);
            var model = new TenancyViewModel() { RoomId = roomId, PersonalIdentityNumber = clientPin };
            if (client != null)
            {
                model.FirstName = client.FirstName;
                model.LastName = client.LastName;
                model.Phone = client.Phone;
                model.Email = client.Email;
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Tenancy(TenancyViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            //TODO: REFACTOR THE CODE!

            var selectedRoom = this.roomsService.GetById<Room>(model.RoomId);

            foreach (var hireDate in selectedRoom.HireDates)
            {
                if (model.AccommodationDate < hireDate.AccommodationDate && model.DepartureDate > hireDate.AccommodationDate)
                {
                    this.ModelState.AddModelError(string.Empty, string.Format(GlobalConstants.InvalidHireDateErrorMsg, model.RoomId, hireDate.AccommodationDate.ToString("d"), hireDate.DepartureDate.ToString("d")));
                    return this.View(model);
                }

                if (model.AccommodationDate >= hireDate.AccommodationDate && model.AccommodationDate.Value.Date < hireDate.DepartureDate.Date)
                {
                    this.ModelState.AddModelError(string.Empty, string.Format(GlobalConstants.InvalidHireDateErrorMsg, model.RoomId, hireDate.AccommodationDate.ToString("d"), hireDate.DepartureDate.ToString("d")));
                    return this.View(model);
                }
            }

            if (this.clientsService.GetClientByPIN(model.PersonalIdentityNumber) == null)
            {
                var newClient = new Client()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PersonalIdentityNumber = model.PersonalIdentityNumber,
                    Phone = model.Phone,
                    Email = model.Email,
                };
                await this .clientsService.AddAsync(newClient);
            }

            var currClient = this.clientsService.GetClientByPIN(model.PersonalIdentityNumber);

            if (model.AccommodationDate == null)
            {
                this.ModelState.AddModelError(string.Empty, string.Format(GlobalConstants.InvalidTenancyDateErrorMsg, "Accommodation"));
                return this.View(model);
            }

            if (model.DepartureDate == null)
            {
                this.ModelState.AddModelError(string.Empty, string.Format(GlobalConstants.InvalidTenancyDateErrorMsg, "Departure"));
                return this.View(model);
            }

            var clientRoom = new ClientRoom()
            {
                AccommodationDate = (DateTime)model.AccommodationDate,
                DepartureDate = (DateTime)model.DepartureDate,
                Client = currClient,
                ClientId = currClient.Id,
                Room = selectedRoom,
                RoomId = model.RoomId,
            };
            
            await this.clientRoomsService.AddAsync(clientRoom);

            return this.Redirect(
                $"/Clients/Success?clientName={currClient.FirstName}&roomId={model.RoomId}" +
                $"&accDate={model.AccommodationDate.Value.ToString("dd.MM.yyyy")}&depDate={model.DepartureDate.Value.ToString("dd.MM.yyyy")}");
        }

        public IActionResult Success(TenancySuccessViewModel model)
        {
            return this.View(model);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClientViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var client = new Client()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PersonalIdentityNumber = model.PersonalIdentityNumber,
                Phone = model.Phone,
                Email = model.Email,
            };

            if (this.clientsService.GetClientByPIN(client.PersonalIdentityNumber) != null)
            {
                this.ModelState.AddModelError(string.Empty, "Client with equal personal identity number exists in the database!");
                return this.View(model);
            }

            await this.clientsService.AddAsync(client);

            return this.RedirectToAction("Success");
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
