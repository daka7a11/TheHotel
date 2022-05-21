using System;
using System.ComponentModel.DataAnnotations;
using TheHotel.Common;
using TheHotel.Data.Models;
using TheHotel.Mapping;

namespace TheHotel.ViewModels.Offers
{
    public class AllOffersViewModel : IMapFrom<Offer>, IMapTo<Offer>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredPropertyErrorMsg)]
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int Discount { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredPropertyErrorMsg)]
        public string Description { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredPropertyErrorMsg)]
        public string ImageUrl { get; set; }
    }
}
