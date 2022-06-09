using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheHotel.Common;
using TheHotel.Data.Models;
using TheHotel.Mapping;
using TheHotel.Services.Employees;
using TheHotel.ViewModels.Employees;

namespace TheHotel.Controllers
{
    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class EmployeesController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmployeesService employeesService;

        public EmployeesController(UserManager<ApplicationUser> userManager,
            IEmployeesService employeesService)
        {
            this.userManager = userManager;
            this.employeesService = employeesService;
        }

        public async Task<IActionResult> Index()
        {
            var model = userManager.Users
                .Where(x => x.IsDeleted == false)
                .Select(x => AutoMapperConfig.MapperInstance.Map<EmployeeViewModel>(x))
                .ToList();

            foreach (var employee in model)
            {
                var currEmployee = await userManager.FindByIdAsync(employee.Id);
                var currEmployeeRoles = await userManager.GetRolesAsync(currEmployee);
                employee.Role = currEmployeeRoles.FirstOrDefault();
            }

            return View(model);
        }

        public async Task<IActionResult> Details(string employeeId)
        {
            var user = await userManager.Users
                .Include(x => x.Reservations)
                .SingleOrDefaultAsync(x => x.Id == employeeId);

            if (user == null)
            {
                return Redirect("/Employees");
            }

            var employee = AutoMapperConfig.MapperInstance.Map<EmployeeViewModel>(user);

            var employeeRoles = await userManager.GetRolesAsync(user);

            employee.Role = employeeRoles.FirstOrDefault();

            return View(employee);
        }

        public async Task<IActionResult> Delete(string employeeId)
        {
            await employeesService.Delete(employeeId);

            return Redirect($"/Employees/Details?employeeId={employeeId}");
        }

        public IActionResult Undelete()
        {
            var deletedEmployees = employeesService.GetDeletedEmployees<EmployeeViewModel>();

            return View(deletedEmployees);
        }

        [HttpPost]
        public async Task<IActionResult>Undelete(string employeeId)
        {
            await employeesService.Undelete(employeeId);

            return Redirect($"/Employees/Details?employeeId={employeeId}");
        }
    }
}
