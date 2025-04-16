using Ikea.DAL.Entities.Employees;
using Ikea.DAL.Persistence.Data;
using Ikea.DAL.Persistence.Repositories._GenericRepo;
using Ikea.DAL.Persistence.Repositories._Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ikea.DAL.Persistence.Repositories.Employees
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {

        public EmployeeRepository(ApplicationDbContext dbContext)
            : base(dbContext)
        {

        }


    }
};
