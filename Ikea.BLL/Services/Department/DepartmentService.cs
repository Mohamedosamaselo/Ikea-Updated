using Ikea.BLL.Models.Departments;
using Ikea.BLL.Services.Interfaces;
using Ikea.DAL.Persistence.Repositories._Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ikea.BLL.Services.Department
{
    public class DepartmentService : IDepartmentService
    {
        #region Fields

        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;


        #endregion

        #region Constructors
        public DepartmentService(/*IDepartmentRepository departmentRepository*/IUnitOfWork unitOfWork)// Ask Clr to Create Object From Department Repo 
        {
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region Methods

        public async Task<IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {
            var DepRepo = _unitOfWork.DepartmentRepository;

            var departments = await DepRepo.GetAllAsIQueryable()
                             .Select(Dep => new DepartmentDto
                             {
                                 Id = Dep.Id,
                                 Name = Dep.Name,
                                 Code = Dep.Code,
                                 CreationDate = Dep.CreationDate,

                             }).AsNoTracking().ToListAsync();

            return departments;
        }

        public async Task<DepartmentDetailsDto?> GetDepartmentByIdAsync(int id)

        {
            var DepartmentRepo = _unitOfWork.DepartmentRepository;

            var Department = await DepartmentRepo.GetByIdAsync(id);

            if (Department is { }) // if Dep is not equall null 
                return new DepartmentDetailsDto()
                {
                    Id = Department.Id,
                    Name = Department.Name,
                    Code = Department.Code,
                    Description = Department.Description,
                    CreationDate = Department.CreationDate,
                    CreatedBy = Department.CreatedBy,
                    CreatedOn = Department.CreatedOn,
                    LastModifiedBy = Department.LastModifiedBy,
                    LastModifiedOn = Department.LastModifiedOn,
                    IsDeleted = Department.IsDeleted,
                };
            return null;
        }

        public async Task<int> CreateDepartmentAsync(CreatedDepartmentDto departmentDto)
        {
            var Department = new DAL.Entities.Departments.Department()
            {
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description!,
                CreationDate = departmentDto.CreationDate,
                //CreatedOn = DateTime.UtcNow,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };
            _unitOfWork.DepartmentRepository.Add(Department);

            return await _unitOfWork.saveChangesAsync();
        }

        public async Task<int> UpdateDepartmentAsync(UpdatedDepartmentDto departmentDto)
        {
            var Department = new DAL.Entities.Departments.Department()
            {
                Id = departmentDto.Id,
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };
            _unitOfWork.DepartmentRepository.Update(Department);

            return await _unitOfWork.saveChangesAsync();
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var departmentRepo = _unitOfWork.DepartmentRepository;

            var department = await departmentRepo.GetByIdAsync(id);

            if (department is { })// if Dep is Not Null 
                departmentRepo.Delete(department);

            return await _unitOfWork.saveChangesAsync() > 0;
        }
    }
    #endregion
}
