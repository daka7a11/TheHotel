using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheHotel.Data.Models;

namespace TheHotel.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Room> Rooms { get; set; }

        public virtual DbSet<RoomType> RoomTypes { get; set; }

        public virtual DbSet<Client> Clients { get; set; }

        public virtual DbSet<ClientRoom> ClientRooms { get; set; }

        public virtual DbSet<Image> Images { get; set; }

        public virtual DbSet<Offer> Offers { get; set; }

        //public virtual DbSet<Question> Questions { get; set; }

        //public virtual DbSet<Review> Reviews { get; set; }
    }
}
