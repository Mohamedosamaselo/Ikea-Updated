using Ikea.DAL.Entities.Departments;
using Ikea.DAL.Persistence.Data;
using Ikea.DAL.Persistence.Repositories._GenericRepo;
namespace Ikea.DAL.Persistence.Repositories.Departments


{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbContext) 
            : base(dbContext)
        {

        }
    }
}
