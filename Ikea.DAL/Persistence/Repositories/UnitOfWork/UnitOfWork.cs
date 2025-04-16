using Ikea.DAL.Entities.Departments;
using Ikea.DAL.Persistence.Data;
using Ikea.DAL.Persistence.Repositories._Interfaces;
using Ikea.DAL.Persistence.Repositories.Departments;
using Ikea.DAL.Persistence.Repositories.Employees;

namespace Ikea.DAL.Persistence.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)// Ask CLR For Createion Object from ApplicationDbContext
        {
            _context = context;
        }


        public IDepartmentRepository DepartmentRepository
        {
            get
            {
                return new DepartmentRepository(_context);
            }
        }

        public IEmployeeRepository EmployeeRepository
        {
            get
            {
                return new EmployeeRepository(_context);
            }
        }

        public async Task<int> saveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
             await _context.DisposeAsync();
        }


    }
}
