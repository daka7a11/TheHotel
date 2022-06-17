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
            ApplicationUser user = null;
            ApplicationRole admRole = null;

            if (!db.Users.Any())
            {
                var hasher = new PasswordHasher<ApplicationUser>();

                user = new ApplicationUser()
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


               
            }

            if (!db.Roles.Any())
            {
                admRole = new ApplicationRole 
                { 
                    Name = "Администратор",
                    NormalizedName = "АДМИНИСТРАТОР" 
                };

                var receptionistRole = new ApplicationRole
                {
                    Name = "Рецепционист",
                    NormalizedName = "РЕЦЕПЦИОНИСТ"
                };

                await db.Roles.AddRangeAsync(admRole, receptionistRole);
            }

            if (!db.UserRoles.Any())
            {
                if (user == null)
                {
                    user = db.Users.FirstOrDefault(x => x.Email == "adm@abv.bg");
                }

                if (admRole == null)
                {
                    admRole = db.Roles.FirstOrDefault(x => x.Name == "Администратор");
                }

                if (user != null && admRole != null)
                {
                    await db.UserRoles.AddAsync(new IdentityUserRole<string>() { UserId = user.Id, RoleId = admRole.Id });

                    await db.SaveChangesAsync();
                }
            }
        }
    }
}
