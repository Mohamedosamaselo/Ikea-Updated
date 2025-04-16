using System.ComponentModel.DataAnnotations;

namespace Ikea.BLL.Models.Employees
{
   public class EmployeeDto
    {

        #region Adminstration 
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        #endregion

        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int? Age { get; set; } = null!;

        [DataType(DataType.Currency)] // just For displaying        
        public decimal Salary { get; set; }
        
        public bool IsActive { get; set; }

        [DataType(DataType.EmailAddress)] // Just For Displaying 
        public string? Email { get; set; }

        public string Gender { get; set; } = null!;

        public string EmployeeType { get; set; } = null!;

      
        public string? Department { get; set; } = null!;// Display EMployee in Which Department in index View 

        public String? Image { get; set; } //to upload  Image of the Employee 


    }
}
