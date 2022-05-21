using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheHotel.Data.Models;
using TheHotel.Data.Repositories;
using TheHotel.Mapping;
using TheHotel.ViewModels.Offers;

namespace TheHotel.Services.Offers
{
    public class OffersService : IOffersService
    {
        private readonly IDeletableEntityRepository<Offer> offersRepository;

        public OffersService(IDeletableEntityRepository<Offer> offersRepository)
        {
            this.offersRepository = offersRepository;
        }

        public async Task AddAsync(CreateOfferViewModel model)
        {
            var offer = AutoMapperConfig.MapperInstance.Map<Offer>(model);
            offer.CreatedOn = DateTime.UtcNow;
            offer.StartDate = model.StartDate.ToUniversalTime();
            if (model.EndDate != null)
            {
                offer.EndDate = model.EndDate.Value.ToUniversalTime();
            }

            await offersRepository.AddAsync(offer);
            await offersRepository.SaveChangesAsync();
        }

        public void Delete(int offerId)
        {
            var offer = offersRepository.All().FirstOrDefault(x => x.Id == offerId);

            if (offer == null)
            {
                return;
            }

            offersRepository.Delete(offer);
            offersRepository.SaveChanges();
        }

        public void Edit(int offerId, AllOffersViewModel model)
        {
            var offer = offersRepository.All().FirstOrDefault(x => x.Id == offerId);

            if (offer == null)
            {
                return;
            }

            offer.Name = model.Name;
            offer.Discount = model.Discount;
            offer.StartDate = model.StartDate;
            offer.EndDate = model.EndDate;
            offer.ImageUrl = model.ImageUrl;
            offer.Description = model.Description;
            offer.ModifiedOn = DateTime.UtcNow;

            offersRepository.SaveChanges();
        }

        public IEnumerable<Offer> GetAll()
        {
            return offersRepository.All()
                .ToList();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return offersRepository.All()
                .To<T>()
                .ToList();
        }

        public Offer GetById(int id)
        {
            return offersRepository.All().FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Offer> GetDeleted()
        {
            return offersRepository.AllWithDeleted()
                .Where(x => x.IsDeleted == true)
                .ToList();
        }

        public IEnumerable<T> GetDeleted<T>()
        {
            return offersRepository.AllWithDeleted()
                .Where(x => x.IsDeleted == true)
                .To<T>()
                .ToList();
        }

        public void Undelete(int offerId)
        {
            var offer = offersRepository.AllWithDeleted().FirstOrDefault(x => x.IsDeleted == true && x.Id == offerId);

            if (offer == null)
            {
                return;
            }

            offersRepository.Undelete(offer);
            offersRepository.SaveChanges();
        }
    }
}
