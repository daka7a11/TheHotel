using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using TheHotel.ViewModels;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using TheHotel.Common;
using Microsoft.AspNetCore.Identity;
using TheHotel.Data.Models;

namespace TheHotel.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration config;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(IConfiguration config,
            UserManager<ApplicationUser> userManager)
        {
            this.config = config;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44375/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response =
               await client.GetAsync($"https://api.openweathermap.org/data/2.5/forecast?lat=1&lon=1&cnt=0&units=metric&lang=en&appid={config.GetSection("OpenWeatherApiKey").Value}");

            List<Weather> weathers = new List<Weather>();

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var parsedObject = JObject.Parse(data);
                var weatherlist = parsedObject["list"];
                for (int i = 3; i < weatherlist.Count(); i += 8)
                {
                    var temp = weatherlist[i]["main"]["temp"].ToString();
                    var humidity = weatherlist[i]["main"]["humidity"].ToString();
                    var windSpeed = weatherlist[i]["wind"]["speed"].ToString();
                    var weather = new Weather
                    {
                        Temp = Double.Parse(temp),
                        Humidity = Double.Parse(humidity),
                        WindSpeed = Double.Parse(windSpeed),
                    };
                    weathers.Add(weather);
                }
            }

            return View(weathers);
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
