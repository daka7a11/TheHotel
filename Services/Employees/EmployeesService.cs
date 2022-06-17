using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheHotel.Data.Models;
using TheHotel.Data.Repositories;
using TheHotel.Mapping;
using TheHotel.ViewModels.Employees;

namespace TheHotel.Services.Employees
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IRepository<IdentityUserRole<string>> userRolesRepository;

        public EmployeesService(
            IDeletableEntityRepository<ApplicationUser> userRepository,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IRepository<IdentityUserRole<string>> userRolesRepository)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.userRolesRepository = userRolesRepository;
        }

        public async Task Delete(string employeeId)
        {
            var employee = userRepository.All().FirstOrDefault(x => x.Id == employeeId);

            if (employee != null)
            {
                userRepository.Delete(employee);
                await userRepository.SaveChangesAsync();
            }
        }

        public void Edit(string employeeId, EditEmployeeViewModel model)
        {
            //Not working with async Task Edit()
            //var user = await userManager.FindByIdAsync(employeeId);
            //await userManager.AddToRoleAsync(user, "Administrator");

            var employee = userManager.FindByIdAsync(employeeId).GetAwaiter().GetResult();
            var newRole = roleManager.FindByIdAsync(model.RoleId).GetAwaiter().GetResult();
            var oldRoles = userManager.GetRolesAsync(employee).GetAwaiter().GetResult();

            if (employee != null)
            {
                employee.FirstName = model.FirstName;
                employee.MiddleName = model.MiddleName;
                employee.LastName = model.LastName;
                employee.PersonalIdentityNumber = model.PersonalIdentityNumber;
                employee.PhoneNumber = model.PhoneNumber;
                employee.ModifiedOn = DateTime.UtcNow;

                userManager.UpdateAsync(employee).GetAwaiter().GetResult();

                if (oldRoles.Count > 0)
                {
                    userManager.RemoveFromRolesAsync(employee, oldRoles).GetAwaiter().GetResult();
                }

                if (newRole != null)
                {
                    userManager.AddToRoleAsync(employee, newRole.Name).GetAwaiter().GetResult();
                }
            }
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllRoles()
        {
            return roleManager.Roles.Select(x => new KeyValuePair<string, string>(x.Id, x.Name)).ToList();
        }

        public IEnumerable<T> GetDeletedEmployees<T>()
        {
            return userRepository.AllWithDeleted()
                .Include(x => x.Reservations)
                .Where(x => x.IsDeleted == true)
                .To<T>()
                .ToList();
        }

        public async Task Undelete(string employeeId)
        {
            var employee = userRepository.AllWithDeleted().FirstOrDefault(x => x.Id == employeeId);

            if (employee != null)
            {
                userRepository.Undelete(employee);
                employee.ModifiedOn = DateTime.UtcNow;
                await userRepository.SaveChangesAsync();
            }
        }
    }
}
