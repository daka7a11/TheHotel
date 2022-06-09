using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheHotel.Data.Models;
using TheHotel.Data.Repositories;
using TheHotel.Mapping;

namespace TheHotel.Services.Employees
{
    public class EmployeesService : IEmployeesService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public EmployeesService(UserManager<ApplicationUser> userManager, 
            IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.userManager = userManager;
            this.userRepository = userRepository;
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
