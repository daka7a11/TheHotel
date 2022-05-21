using System;
using TheHotel.Data.Models;
using TheHotel.Mapping;

namespace TheHotel.ViewModels.Offers
{
    public class UndeleteViewModel : IMapFrom<Offer>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Discount { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
