using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheHotel.Services.Employees
{
    public interface IEmployeesService
    {
        Task Delete(string employeeId);

        Task Undelete(string employeeId);

        IEnumerable<T> GetDeletedEmployees<T>();
    }
}
