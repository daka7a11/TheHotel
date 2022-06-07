using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TheHotel.Common;
using TheHotel.Data.Models;
using TheHotel.Mapping;
using TheHotel.Services.Reviews;
using TheHotel.ViewModels.Reviews;

namespace TheHotel.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewsService reviewsService;

        public ReviewsController(IReviewsService reviewsService)
        {
            this.reviewsService = reviewsService;
        }

        public IActionResult Index(int page = 1)
        {
            if (page <= 0)
            {
                return RedirectToAction("Index");
            }

            var reviews = reviewsService.Get5Reviews<ReviewsViewModel>(page);
            var totalPages = reviewsService.GetTotalPages();
            var averageRating = reviewsService.GetAverageRating();

            if (page > totalPages)
            {
                return Redirect($"/Reviews?page={totalPages}");
            }

            var model = new IndexReviewsViewModel
            {
                Reviews = reviews,
                CurrentPage = page,
                TotalPages = totalPages,
                AverageRating = averageRating,
            };

            return View(model);
        }

        public IActionResult CreateReview()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview(CreateReviewViewModel model)
        {
            if (model.Rating <= 0 || model.Rating > 5)
            {
                ModelState.AddModelError(string.Empty, GlobalConstants.InvalidRating);
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await reviewsService.AddAsync(model);

            TempData["SuccessfullyCreatedReview"] = GlobalConstants.SuccessfullyCreatedReview;

            return Redirect("/Reviews");
        }
    }
}
