using Ikea.BLL.Models.Departments;

namespace Ikea.BLL.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync();

        Task<DepartmentDetailsDto?> GetDepartmentByIdAsync(int id);

        Task<int> CreateDepartmentAsync(CreatedDepartmentDto departmentDto);

        Task<int> UpdateDepartmentAsync(UpdatedDepartmentDto departmentDto);

        Task<bool> DeleteDepartmentAsync(int id);



    }
}
