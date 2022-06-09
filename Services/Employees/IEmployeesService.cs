using System.Threading.Tasks;

namespace TheHotel.Services.Employees
{
    public interface IEmployeesService
    {
        Task Delete(string employeeId);
    }
}
