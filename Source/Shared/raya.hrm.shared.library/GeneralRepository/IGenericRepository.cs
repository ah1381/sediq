using System.Linq.Expressions;

namespace Raya.Hrm.Shared.Library.GeneralRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(long id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<long> AddAsync(T entity);
        Task<List<long>> AddListAsync(List<T> entities);
        Task UpdateAsync(T entity);
        Task DeleteAsync(long id);
        Task<IEnumerable<T>> QueryAsync(string sql, object? parameters = null);
        IQueryable<T> GetQueryable();
    }
}
