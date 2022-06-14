using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TheHotel.Data.Models;

namespace TheHotel.Common
{
    public static class GlobalMethods
    {
        public static bool IsRoomAvailable(Room room, DateTime? accDate, DateTime? depDate)
        {
            foreach (var hireDate in room.HireDates.Where(x => x.IsConfirmed == true && x.IsDeleted == false))
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

        public static IEnumerable<string> GetImages(string webRootPath, string folderName)
        {
            var folderPath = Path.Combine(webRootPath, "img", folderName.ToString());

            Directory.CreateDirectory(folderPath);

            var imagesPath = Directory.GetFiles(folderPath);

            List<string> imgSrcs = new List<string>();

            foreach (var filePath in imagesPath)
            {
                var imgPath = filePath.Split("\\");

                var imgSrc = imgPath.TakeLast(3)
                    .ToArray();

                imgSrcs.Add($"/{imgSrc[0]}/{imgSrc[1]}/{imgSrc[2]}");
            }

            return imgSrcs;
        }

        public static void AddImages(string webRootPath, string folderName, IEnumerable<IFormFile> images)
        {
            string imgsPath = Path.Combine(webRootPath, "img", folderName);

            Directory.CreateDirectory(imgsPath);

            foreach (var image in images)
            {
                string filePath = Path.Combine(imgsPath, $"{Guid.NewGuid().ToString()}.{image.FileName.Split(".").TakeLast(1).FirstOrDefault()}");

                using Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);

                image.CopyTo(fileStream);
            }
        }
    }
}
