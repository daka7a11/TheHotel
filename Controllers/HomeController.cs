using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TheHotel.Services.ClientRooms;
using TheHotel.ViewModels;

namespace TheHotel.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClientRoomsService clientRoomsService;

        public HomeController(IClientRoomsService clientRoomsService)
        {
            this.clientRoomsService = clientRoomsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Offers()
        {
            return View();
        }

        public IActionResult Gallery()
        {
            return View();
        }

        public IActionResult Contacts()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
