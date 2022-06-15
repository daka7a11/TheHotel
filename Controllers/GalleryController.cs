using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TheHotel.Common;
using TheHotel.ViewModels.Gallery;

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
        public IActionResult Index(int page=1)
        {
            if (page <= 0)
            {
                return RedirectToAction(nameof(Index));
            }

            var galleryImgsSrc = GlobalMethods.GetImages(env.WebRootPath, "Gallery");

            //15 images per page
            int totalPages = (int)Math.Ceiling(galleryImgsSrc.Count() * 1.00 / 15);

            if (page > totalPages)
            {
                return Redirect($"/Gallery/Index?page={totalPages}");
            }

            var images = galleryImgsSrc.Skip((page * 15) - 15)
                .Take(15);

            var model = new GalleryIndexViewModel
            {
                Images = images,
                Page = page,
                TotalPages = totalPages,
            };

            return View(model);
        }

        public IActionResult AddImages()
        {
            return View();
        }

        [HttpPost]
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
