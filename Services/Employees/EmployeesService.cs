using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheHotel.Data.Models;
using TheHotel.Data.Repositories;

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
    }
}
