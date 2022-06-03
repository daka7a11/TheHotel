using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Net.Http;
using TheHotel.ViewModels;

namespace TheHotel.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public HomeController(IHttpContextAccessor httpContextAccessor,
            HttpClient httpClient,
            IConfiguration configuration)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            httpClient.BaseAddress = new Uri(@"https://localhost:44375");

            var response = httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/forecast/daily?lat=1&lon=1&cnt=5&appid={configuration.GetSection("OpenWeatherApiKey").Value}").GetAwaiter().GetResult();

            var msg = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

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
