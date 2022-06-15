using System.Collections.Generic;

namespace TheHotel.ViewModels.Gallery
{
    public class GalleryIndexViewModel
    {
        public IEnumerable<string> Images { get; set; }

        public int Page { get; set; }

        public int TotalPages { get; set; }
    }
}
