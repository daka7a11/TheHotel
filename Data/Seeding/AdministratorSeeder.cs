using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheHotel.Data.Models;

namespace TheHotel.Data.Seeding
{
    public class AdministratorSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext db)
        {
            if (!db.Users.Any())
            {
                var hasher = new PasswordHasher<ApplicationUser>();

                var user = new ApplicationUser()
                {
                    FirstName = "Admin",
                    MiddleName = "Adminov",
                    LastName = "Adminov",
                    PersonalIdentityNumber = "1122334455",
                    UserName = "adm@abv.bg",
                    NormalizedUserName = "ADM@ABV.BG",
                    Email = "adm@abv.bg",
                    NormalizedEmail = "ADM@ABV.BG",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                user.PasswordHash = hasher.HashPassword(user, "Admin123!");

                await db.Users.AddAsync(user);

                var role = await db.Roles.AddAsync(new ApplicationRole() { Name = "Administrator", NormalizedName = "ADMINISTRATOR" });

                await db.UserRoles.AddAsync(new IdentityUserRole<string>() { UserId = user.Id, RoleId = role.Entity.Id });

                await db.SaveChangesAsync();


            }
        }
    }
}
