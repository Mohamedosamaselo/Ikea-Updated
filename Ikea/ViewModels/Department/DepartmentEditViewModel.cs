using System.ComponentModel.DataAnnotations;

namespace Ikea.PL.ViewModels.Department
{
    public class DepartmentEditViewModel
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Description { get; set; } = null!;

        [Display(Name ="date of Creation")]
        public DateTime CreationDate { get; set; }

    }
}

