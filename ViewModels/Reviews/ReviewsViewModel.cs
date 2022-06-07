using System;
using TheHotel.Data.Models;
using TheHotel.Mapping;

namespace TheHotel.ViewModels.Reviews
{
    public class ReviewsViewModel : IMapFrom<Review>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int Rating { get; set; }

        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
