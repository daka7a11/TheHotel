using System.Collections.Generic;
using System.Threading.Tasks;
using TheHotel.Data.Models;
using TheHotel.ViewModels.Offers;

namespace TheHotel.Services.Offers
{
    public interface IOffersService
    {
        IEnumerable<Offer> GetAll();
        IEnumerable<T> GetAll<T>();
        IEnumerable<Offer> GetDeleted();
        IEnumerable<T> GetDeleted<T>();

        Offer GetById(int id);
        Task AddAsync(CreateOfferViewModel model);
        void Edit(int offerId, AllOffersViewModel model);
        void Delete(int offerId);
        void Undelete(int offerId);
    }
}
