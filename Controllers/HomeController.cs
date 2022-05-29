using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TheHotel.Data.Models;
using TheHotel.EmailSender;
using TheHotel.EmailSender.ViewRender;
using TheHotel.Services.ClientRooms;
using TheHotel.ViewModels;

namespace TheHotel.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public HomeController(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateCookie()
        {
            string key = "Key_Cookie";
            string value = "Value_Cookie";

            CookieOptions option = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddMinutes(2)
            };

            httpContextAccessor.HttpContext.Response.Cookies.Append(key,value,option);

            return View("Index");
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
