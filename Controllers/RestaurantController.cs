using Microsoft.AspNetCore.Mvc;

namespace TheHotel.Controllers
{
    public class RestaurantController : Controller
    {
        public IActionResult Home()
        {
            return this.View();
        }

        public IActionResult Menu()
        {
            return this.View();
        }
    }
}
