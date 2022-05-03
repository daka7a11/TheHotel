using System.Linq;
using System.Threading.Tasks;
using TheHotel.Data.Models;

namespace TheHotel.Data.Seeding
{
    public class ImageSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext db)
        {
            if (db.Images.Any())
            {
                return;
            }

            await db.Images.AddRangeAsync(
                new Image() { RoomId = 1, Url = "https://romantika-bg.com/images/Romantika_S3_IMG_0843.jpg" },
                new Image() { RoomId = 1, Url = "https://www.vazrozhdentsi.com/uploads/images/big/46_Hotel_Vazrozhdentsi_Double_room_08.jpg" },
                new Image() { RoomId = 2, Url = "https://parkhotelpirin.com/sites/default/files/styles/carousel_1500x1000/public/rooms/park-hotel-pirin-dvoina-staya-standart-1.jpg" },
                new Image() { RoomId = 3, Url = "https://festahotels.com/page_images/1-100/75_SGL%20Room%20(2).jpg" },
                new Image() { RoomId = 4, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT_wed5ILkXW-JwpMKiVkuTqUc0lXpQENmEp8mP6d9QaYjPVNCCMsg1c8EdpQqnYu6EoTA&usqp=CAU" },
                new Image() { RoomId = 5, Url = "https://hotel-stz.com/wp-content/uploads/2019/08/bbh_doublestandart_twin-8-2-1024x683.jpg" },
                new Image() { RoomId = 6, Url = "https://hotelregattapalace.com/uploads/tmp/_w920_h615_pp/6fbbf7bd14c39ab1e0c253c1b346f489.jpg" },
                new Image() { RoomId = 7, Url = "https://hotelviena-bg.com/wp-content/uploads/2016/01/SingleRoom.jpg" },
                new Image() { RoomId = 8, Url = "https://festahotels.com/page_images/1-100/75_SGL%20Room%20(5).jpg" },
                new Image() { RoomId = 9, Url = "https://www.panorama-kiten.com/wp-content/uploads/2021/01/%D1%81%D1%82%D0%B0%D1%8F-1.jpg" },
                new Image() { RoomId = 10, Url = "https://www.panorama-kiten.com/wp-content/uploads/2021/01/%D1%81%D1%82%D0%B0%D1%8F-1.jpg" },
                new Image() { RoomId = 11, Url = "https://103degrees.clientric.website/storage/thumbs/613783558a824b80f84ab79e74e479fb_1320x882_80_1551088012_thumbnail_adb115059e28d960fa8badfac5516667.jpg" },
                new Image() { RoomId = 12, Url = "https://romantika-bg.com/images/Romantika_S3_IMG_0843.jpg" },
                new Image() { RoomId = 13, Url = "https://magnific.bg/wp-content/uploads/2019/12/DSC_7625-1350x920.jpg" },
                new Image() { RoomId = 14, Url = "https://103degrees.clientric.website/storage/thumbs/613783558a824b80f84ab79e74e479fb_1320x882_80_1551088012_thumbnail_adb115059e28d960fa8badfac5516667.jpg" },
                new Image() { RoomId = 15, Url = "https://romantika-bg.com/images/Romantika_S3_IMG_0843.jpg" }
                );
        }
    }
}
