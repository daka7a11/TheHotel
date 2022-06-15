using AspNetCore.ReCaptcha;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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
                return RedirectToAction(nameof(Index));
            }

            var reviews = reviewsService.Get5Reviews<ReviewsViewModel>(page);
            var totalPages = reviewsService.GetTotalPages();
            double averageRating = 0;
            if (reviews.Count() > 0)
            {
                averageRating = reviewsService.GetAverageRating();
            }

            if (page > totalPages && totalPages > 0)
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

        [ValidateReCaptcha(ErrorMessage = GlobalConstants.InvalidRecaptchaErrorMsg)]
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
