using System;
using System.Linq;
using TheHotel.Data.Models;

namespace TheHotel.Common
{
    public static class GlobalMethods
    {
        public static bool IsRoomAvailable(Room room, DateTime? accDate, DateTime? depDate)
        {
            foreach (var hireDate in room.HireDates.Where(x => x.IsConfirmed == true))
            {
                if (accDate < hireDate.AccommodationDate.Date && depDate > hireDate.AccommodationDate.Date)
                {
                    return false;
                }

                if (accDate >= hireDate.AccommodationDate.Date && accDate.Value.Date < hireDate.DepartureDate.Date)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
