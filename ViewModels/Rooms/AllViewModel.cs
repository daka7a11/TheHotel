using System.Collections.Generic;

namespace TheHotel.ViewModels.Rooms
{
    public class AllViewModel
    {
        public IEnumerable<AllRoomsViewModel> AllRoomsViewModel { get; set; }

        public AvailableRoomsViewModel AvailableRoomsViewModel { get; set; }
    }
}
