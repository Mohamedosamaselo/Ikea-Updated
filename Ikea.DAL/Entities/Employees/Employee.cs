using Ikea.DAL.Common.Enums;
using Ikea.DAL.Entities.Departments;
using Microsoft.AspNetCore.Http;

namespace Ikea.DAL.Entities.Employees
{
    public class Employee : ModelBase
    {
        public string  Name { get; set; }
        public int?    Age { get; set; } 
        public string? Address { get; set; }
        public decimal Salary { get; set; }
        public string? PhoneNumber { get; set; }
        public bool    IsActive { get; set; }
        public string? Email { get; set; }
        public DateOnly HiringDate { get; set; }
        public Gender   Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }


        //// F.k of the Entity Employee that Represent Department , Optional Like Navigational Property "Department ? "
        public int? DepartmentId { get; set; }

       // Navigational Property [one] [Related Data]by Default willbe Not Loaded 
        public virtual Department? Department { get; set; }

        public string? Image { get; set; } // For Uploading the name of image of Employee 
    }
}
