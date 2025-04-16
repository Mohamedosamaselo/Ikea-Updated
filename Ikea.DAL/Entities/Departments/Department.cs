using Ikea.DAL.Entities.Employees;
using System.ComponentModel.DataAnnotations;

namespace Ikea.DAL.Entities.Departments
{
    public class Department : ModelBase
    {

        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreationDate { get; set; }


        // Navigational Property[ Many ]
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
        
    }
}
