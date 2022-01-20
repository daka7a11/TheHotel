using System;

namespace TheHotel.ViewModels.Rooms
{
    public class AvailableRoomsViewModel
    {
        public AvailableRoomsViewModel()
        {
            AccommodationDate = null;
            DepartureDate = null;
        }

        public DateTime? AccommodationDate { get; set; }

        public DateTime? DepartureDate { get; set; }
    }
}
