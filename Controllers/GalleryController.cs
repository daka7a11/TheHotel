using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TheHotel.Common;

namespace TheHotel.Controllers
{

    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class GalleryController : Controller
    {
        private readonly IWebHostEnvironment env;

        public GalleryController(IWebHostEnvironment env)
        {
            this.env = env;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var galleryImgsSrc = GlobalMethods.GetImages(env.WebRootPath, "Gallery");

            return View(galleryImgsSrc);
        }

        public IActionResult AddImages()
        {
            return View();
        }

        [HttpPost]
        [RequestSizeLimit(40000000)]
        public IActionResult AddImages(IEnumerable<IFormFile> images)
        {
            if (images.Count() == 0)
            {
                ModelState.AddModelError(string.Empty, GlobalConstants.RequiredImageErrorMsg);
                return View();
            }

            GlobalMethods.AddImages(env.WebRootPath, "Gallery", images);

            return RedirectToAction(nameof(Index));
        }
    }
}
