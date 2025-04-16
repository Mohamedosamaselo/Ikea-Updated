using Ikea.DAL.Common.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ikea.BLL.Models.Employees
{
   public class UpdatedEmployeeDto
    {

        public int Id { get; set; }


        [Required]
        [MaxLength(50, ErrorMessage = "MaxLength of Name is 50 char ")]
        [MinLength(5, ErrorMessage = "MinLength of Name is 5 char ")]
        public string Name { get; set; } = null!;


        [Range(18, 50)]
        public int? Age { get; set; } = null!;


        [RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
          ErrorMessage = "Address Must Be Like 123-Street-City-Country ")]
        public string? Address { get; set; }

        public decimal Salary { get; set; }

        [EmailAddress] // Aplication Validation 
        public string? Email { get; set; }

        [Phone]
        [DisplayName("Phone Number")]
        public string? PhoneNumber { get; set; }

        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

        [DisplayName("Hiring Date")]
        public DateOnly HiringDate { get; set; }

        public Gender Gender { get; set; } 

        public EmployeeType EmployeeType { get; set; }

        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }


        public String? Image { get; set; } //to upload  Image of the Employee 


    }
}
