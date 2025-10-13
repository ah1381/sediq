using Raya.Hrm.Shared.Library.Models;
using Raya.Hrm.Shared.Library.ModelS;

namespace Raya.Hrm.Shared.Library.GeneralRepository
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(long id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T entity); // Return inserted ID
        Task<List<T>> AddListAsync(List<T> entities); // Return inserted ID
        Task UpdateAsync(T entity);
        Task DeleteAsync(long id);
        Task<IEnumerable<T>> QueryAsync(string sql, object? parameters = null);
        IQueryable<T> GetQueryable();
        Task<IEnumerable<T>> GetFilteredAsync(FilterPagedListParameter<T>? filters);
    }
}
