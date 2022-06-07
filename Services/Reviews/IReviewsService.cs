using System.Collections.Generic;
using System.Threading.Tasks;
using TheHotel.ViewModels.Reviews;

namespace TheHotel.Services.Reviews
{
    public interface IReviewsService
    {
        IEnumerable<T> Get5Reviews<T>(int page);

        Task AddAsync(CreateReviewViewModel model);

        int GetTotalPages();

        double GetAverageRating();
    }
}
