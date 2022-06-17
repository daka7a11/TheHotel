using System.Collections.Generic;
using System.Threading.Tasks;
using TheHotel.ViewModels.Employees;

namespace TheHotel.Services.Employees
{
    public interface IEmployeesService
    {
        void Edit(string employeeId, EditEmployeeViewModel model);

        Task Delete(string employeeId);

        Task Undelete(string employeeId);

        IEnumerable<T> GetDeletedEmployees<T>();

        IEnumerable<KeyValuePair<string, string>> GetAllRoles();
    }
}
