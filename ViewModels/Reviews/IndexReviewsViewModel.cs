using System.Collections.Generic;

namespace TheHotel.ViewModels.Reviews
{
    public class IndexReviewsViewModel
    {
        public IEnumerable<ReviewsViewModel> Reviews { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public double AverageRating { get; set; }
    }
}
