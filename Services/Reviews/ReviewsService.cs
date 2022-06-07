using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheHotel.Data.Models;
using TheHotel.Data.Repositories;
using TheHotel.Mapping;
using TheHotel.ViewModels.Reviews;

namespace TheHotel.Services.Reviews
{
    public class ReviewsService : IReviewsService
    {
        private readonly IDeletableEntityRepository<Review> reviewsRepository;

        public ReviewsService(IDeletableEntityRepository<Review> reviewsRepository)
        {
            this.reviewsRepository = reviewsRepository;
        }

        public async Task AddAsync(CreateReviewViewModel model)
        {
            var review = AutoMapperConfig.MapperInstance.Map<Review>(model);
            review.CreatedOn = DateTime.UtcNow;

            if (reviewsRepository.All().Any(x => x.Email == model.Email))
            {
                await ReplaceAsync(review);
            }
            else
            {
                await reviewsRepository.AddAsync(review);
                await reviewsRepository.SaveChangesAsync();
            }
        }

        public IEnumerable<T> Get5Reviews<T>(int page)
        {
            return reviewsRepository.All()
                .Skip((page * 5) - 5)
                .Take(5)
                .To<T>()
                .ToList();
        }

        private async Task ReplaceAsync(Review review)
        {
            var dbReview = reviewsRepository.All().FirstOrDefault(x => x.Email == review.Email);
            dbReview.FirstName = review.FirstName;
            dbReview.LastName = review.LastName;
            dbReview.Rating = review.Rating;
            dbReview.CreatedOn = review.CreatedOn;
            dbReview.Text = review.Text;
            await reviewsRepository.SaveChangesAsync();
        }
    }
}
