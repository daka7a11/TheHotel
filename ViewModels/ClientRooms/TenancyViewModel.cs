﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TheHotel.Common;
using TheHotel.Data.Models;
using TheHotel.Mapping;

namespace TheHotel.ViewModels.Clients
{
    public class TenancyViewModel : IValidatableObject, IMapFrom<Client>, IMapTo<Client>, IMapTo<ClientRoom>, IHaveCustomMappings
    {
        [Required(ErrorMessage = GlobalConstants.RequiredNameErrorMsg)]
        [MinLength(GlobalConstants.ClientNameMinLength, ErrorMessage = GlobalConstants.ClientNameLengthErrorMsg)]
        [MaxLength(GlobalConstants.ClientNameMaxLength, ErrorMessage = GlobalConstants.ClientNameLengthErrorMsg)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredLastNameErrorMsg)]
        [MinLength(GlobalConstants.ClientNameMinLength, ErrorMessage = GlobalConstants.ClientNameLengthErrorMsg)]
        [MaxLength(GlobalConstants.ClientNameMaxLength, ErrorMessage = GlobalConstants.ClientNameLengthErrorMsg)]
        public string LastName { get; set; }


        [MinLength(GlobalConstants.PINMinLength, ErrorMessage = GlobalConstants.PersonalIdentityNumberErrorMsg)]
        [MaxLength(GlobalConstants.PINMaxLength, ErrorMessage = GlobalConstants.PersonalIdentityNumberErrorMsg)]
        public string PersonalIdentityNumber { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredPhoneErrorMsg)]
        public string Phone { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredEmailErrorMsg)]
        [EmailAddress]
        public string Email { get; set; }

        public int RoomId { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredAccommodationDateErrorMsg)]
        [DataType(DataType.Date)]
        public DateTime? AccommodationDate { get; set; }

        [Required(ErrorMessage = GlobalConstants.RequiredDepartureDateErrorMsg)]
        [DataType(DataType.Date)]
        public DateTime? DepartureDate { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.DepartureDate <= this.AccommodationDate)
            {
                yield return new ValidationResult(GlobalConstants.TenancyDateValidateErrorMsg);
            }
        }
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<TenancyViewModel, ClientRoom>()
                .ForMember(x => x.AccommodationDate, y => y.MapFrom(x => (DateTime)x.AccommodationDate))
                .ForMember(x => x.DepartureDate, y => y.MapFrom(x => (DateTime)x.DepartureDate));
        }
    }


}
