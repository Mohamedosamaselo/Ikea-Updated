using AutoMapper;
using Humanizer;
using Ikea.BLL.Models.Departments;
using Ikea.PL.ViewModels.Department;

namespace Ikea.PL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Department 

            CreateMap<DepartmentEditViewModel, CreatedDepartmentDto>();

            CreateMap<DepartmentDetailsDto, DepartmentEditViewModel>();

            CreateMap<DepartmentEditViewModel, UpdatedDepartmentDto>();

            #endregion

        }



    }
}
