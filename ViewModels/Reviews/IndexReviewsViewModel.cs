using System.Collections.Generic;

namespace TheHotel.ViewModels.Reviews
{
    public class IndexReviewsViewModel
    {
        public IEnumerable<ReviewsViewModel> Reviews { get; set; }

        public int Page { get; set; }
    }
}
