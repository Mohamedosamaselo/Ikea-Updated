using Ikea.DAL.Entities;
using Ikea.DAL.Persistence.Data;
using Ikea.DAL.Persistence.Repositories._Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ikea.DAL.Persistence.Repositories._GenericRepo
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {

        #region Fields

        private readonly ApplicationDbContext _dbContext;

        #endregion

        #region Constructors
        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods

        public async Task<IEnumerable<T>> GetAllAsync(bool AsNoTracking = true)
        {

            if (AsNoTracking)
                return await _dbContext.Set<T>().Where(X => !X.IsDeleted).AsNoTracking().ToListAsync();

            return await _dbContext.Set<T>().Where(X => !X.IsDeleted).ToListAsync();

        }

        public IQueryable<T> GetAllAsIQueryable()
        {
            return _dbContext.Set<T>().Where(X => !X.IsDeleted);
        }

        public IEnumerable<T> GetAllAsIEnumerable()
        {
            return _dbContext.Set<T>().Where(X => !X.IsDeleted);
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);// Search Localy First then goes to Database if it doesnot Find the Department 
        
        }


        public void Add(T entity)
        {
             _dbContext.Set<T>().Add(entity);
            //return _dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            //  return _dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {   // Soft Delete
            entity.IsDeleted = true;
            _dbContext.Set<T>().Update(entity);
            //return _dbContext.SaveChanges();

        }


        #endregion
    }
}



