using Ikea.DAL.Entities;

namespace Ikea.DAL.Persistence.Repositories._Interfaces
{
   public interface IGenericRepository<T> where T : ModelBase
    {
        Task<T?> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync(bool AsNoTracking = true);

        IQueryable<T> GetAllAsIQueryable();

        IEnumerable<T> GetAllAsIEnumerable();

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
