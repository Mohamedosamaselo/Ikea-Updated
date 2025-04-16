using System.ComponentModel.DataAnnotations;

namespace Ikea.BLL.Models.Departments
{
    public class CreatedDepartmentDto
    {
        [Required(ErrorMessage = " Name is Required ")]
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public DateTime CreationDate { get; set; }
    }
}
