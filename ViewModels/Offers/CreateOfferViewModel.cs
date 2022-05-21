using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TheHotel.Common;
using TheHotel.Data.Models;
using TheHotel.Mapping;

namespace TheHotel.ViewModels.Offers
{
    public class CreateOfferViewModel : IValidatableObject,IMapFrom<Offer> , IMapTo<Offer>
    {
        [Required(ErrorMessage = GlobalConstants.RequiredPropertyErrorMsg)]
        public string Name { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredPropertyErrorMsg)]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; } = null;

        [Required(ErrorMessage = GlobalConstants.RequiredPropertyErrorMsg)]
        public string ImageUrl { get; set; }

        public int Discount { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndDate != null)
            {
                if (EndDate <= StartDate)
                {
                    yield return new ValidationResult(GlobalConstants.OfferDateValidateErrorMsg);
                }
            }
        }
    }
}
