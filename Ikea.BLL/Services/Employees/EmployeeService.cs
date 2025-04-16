using Ikea.BLL.Common.InfrastructureServices.Attachments;
using Ikea.BLL.Models.Employees;
using Ikea.BLL.Services.Interfaces;
using Ikea.DAL.Entities.Employees;
using Ikea.DAL.Persistence.Repositories._Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Ikea.BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        #region Services

        // private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentService _attachmentService;

        public EmployeeService(/*IEmployeeRepository employeeRepository,*/
                                 IUnitOfWork unitOfWork,
                                 IAttachmentService attachmentService
                               )
        {
            //_employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _attachmentService = attachmentService;
        }

        #endregion
      
        
        
        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(string Search)
        {
            var EmployeeRepo = _unitOfWork.EmployeeRepository;

            var Query =await EmployeeRepo.GetAllAsIQueryable()
                                   .Where(Emp => !Emp.IsDeleted && (string.IsNullOrEmpty(Search) || Emp.Name.ToLower().Contains(Search.ToLower())))
                                   .Include(Employee => Employee.Department)//Eager Loading 
                                   .Select(employee => new EmployeeDto()
                                   {
                                       Id = employee.Id,
                                       Name = employee.Name,
                                       Age = employee.Age,
                                       Salary = employee.Salary,
                                       IsActive = employee.IsActive,
                                       Email = employee.Email,
                                       Gender = employee.Gender.ToString(),
                                       EmployeeType = employee.EmployeeType.ToString(),
                                       Department = employee.Department!.Name
                                   }).ToListAsync();

            // var employee = Query.ToList();// ToList : is a Exctention Method for IEnumerable and any thing that enherit from i Enumerable 

            return Query;

        }

        public async Task<EmployeeDetailsDto> GetEmployeeByIdAsync(int id)
        {
            var employee =await _unitOfWork.EmployeeRepository.GetByIdAsync(id);

            if (employee is { })// if it is  !Null

                return new EmployeeDetailsDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    Salary = employee.Salary,
                    PhoneNumber = employee.PhoneNumber,
                    IsActive = employee.IsActive,
                    Email = employee.Email,
                    HiringDate = employee.HiringDate,
                    Gender = employee.Gender,
                    EmployeeType = employee.EmployeeType,
                    CreatedBy = 1,
                    LastModifiedBy = 1,
                    LastModifiedOn = DateTime.UtcNow,
                    Department = employee.Department?.Name,
                    Image = employee.Image,
                };

            return null!;

        }

        public async Task<int> CreateEmployeeAsync(CreatedEmployeeDto employeeDto)
        {
            
            var employee = new Employee() //1. Mapping from CreatedEMployeeDto To Employee
            {
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                Salary = employeeDto.Salary,
                PhoneNumber = employeeDto.PhoneNumber,
                IsActive = employeeDto.IsActive,
                Email = employeeDto.Email,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                DepartmentId = employeeDto.DepartmentId,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };
            // Uploading Image
            if (employeeDto.Image is not null)
                employee.Image =await _attachmentService.UploadAsync(employeeDto.Image, "Images");
            
            // Add
            // Update 
            // Delete

            // 2. Add Employee in employeeRepository  
            _unitOfWork.EmployeeRepository.Add(employee);

            return await _unitOfWork.saveChangesAsync();

        }

        public async Task<int> UpdateEmployeeAsync(UpdatedEmployeeDto updatedEmployeeDto)
        {
            //1. Mapping from CreatedEMployeeDto To Employee

            var Employee = new Employee()
            {
                Id = updatedEmployeeDto.Id,
                Name = updatedEmployeeDto.Name,
                Age = updatedEmployeeDto.Age,
                Address = updatedEmployeeDto.Address,
                Salary = updatedEmployeeDto.Salary,
                PhoneNumber = updatedEmployeeDto.PhoneNumber,
                IsActive = updatedEmployeeDto.IsActive,
                Email = updatedEmployeeDto.Email,
                HiringDate = updatedEmployeeDto.HiringDate,
                Gender = updatedEmployeeDto.Gender,
                //Image = updatedEmployeeDto.Image,
                EmployeeType = updatedEmployeeDto.EmployeeType,
                DepartmentId = updatedEmployeeDto.DepartmentId,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };

            // 2. Update Employee in employeeRepository  
            _unitOfWork.EmployeeRepository.Update(Employee);

            return await _unitOfWork.saveChangesAsync();
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employeeRepo = _unitOfWork.EmployeeRepository;// i make a variable to create just only one object from EmployeeRepo
            // 1. Find Employee By Id 
            var employee = await employeeRepo.GetByIdAsync(id);

            // 2 . then Check on Employee then Delete 
            if (employee is { })
                employeeRepo.Delete(employee);

            return await _unitOfWork.saveChangesAsync() > 0;
        }

      
    }
}
