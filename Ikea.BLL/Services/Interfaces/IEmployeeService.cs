using Ikea.BLL.Models.Employees;
using Ikea.DAL.Entities.Employees;

namespace Ikea.BLL.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(string Search);

        Task<EmployeeDetailsDto> GetEmployeeByIdAsync(int id);

        Task<int> CreateEmployeeAsync(CreatedEmployeeDto entity);

        Task<int> UpdateEmployeeAsync(UpdatedEmployeeDto entity);

        Task<bool> DeleteEmployeeAsync(int id); 


    }  
}
