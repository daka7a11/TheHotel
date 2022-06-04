using Microsoft.AspNetCore.Mvc;

namespace TheHotel.Controllers
{
    public class ReviewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
