using System.Linq.Expressions;

namespace DMS.Web.Repositories.Interfaces
{ 
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? IncludeProperties = null);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? IncludeProperties = null, bool tracked = false);
        Task<T> GetOneAsync(Expression<Func<T, bool>> filter, string? IncludeProperties = null, bool tracked = false);
        T GetOne(Expression<Func<T, bool>> filter, string? IncludeProperties = null);
        Task AddAsync(T entity);
        void Remove(T entity);
    }
}
