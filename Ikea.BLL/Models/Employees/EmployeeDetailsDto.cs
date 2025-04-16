using Ikea.DAL.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Ikea.BLL.Models.Employees
{
     public class EmployeeDetailsDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? Age { get; set; } 

        public decimal Salary { get; set; }

        
        [DataType(DataType.Currency)]
        public string? Address { get; set; }
        
        
        [Display(Name = "IS Active")]
        public bool IsActive { get; set; }
        
        
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        
        
        
        [Display(Name = "Phone Number ")]
        public string? PhoneNumber { get; set; }
        
        
        
        [Display(Name = "Hiring Date ")]
        public DateOnly HiringDate { get; set; }


        public Gender Gender { get; set; }

        public EmployeeType EmployeeType { get; set; } 



        #region Adminstration Data
        public int CreatedBy { get; set; }
        public DateTime Createdon { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        #endregion

        public string? Department { get; set; } = null; // To Display Department  in Details View 

        public string? Image { get; set; } // to Upload Image of the Employee 


    }
}
