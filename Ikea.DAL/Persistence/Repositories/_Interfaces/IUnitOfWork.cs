using Ikea.DAL.Entities.Departments;

namespace Ikea.DAL.Persistence.Repositories._Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable// to inherit DisposeAsync
    {
        // [ Signature for  Property for each and every Repository ]  

        public IDepartmentRepository DepartmentRepository { get;  }

        public IEmployeeRepository EmployeeRepository { get; }

        public Task<int> saveChangesAsync();

       

    }
}
