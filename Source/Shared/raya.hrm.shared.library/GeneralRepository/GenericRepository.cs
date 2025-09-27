using Dapper;
using Microsoft.EntityFrameworkCore;
using Raya.Hrm.Shared.Library.ModelS;
using System.Data;

namespace Raya.Hrm.Shared.Library.GeneralRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly DbContext _context;
        private readonly IDbConnection _connection;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _connection = context.Database.GetDbConnection();
        }

        public async Task<T?> GetByIdAsync(long id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
                throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with Id {id} was not found.");
            if (entity?.Status == 0)
                return null; // Exclude soft-deleted entities
            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>()
                .Where(e => e.Status != 0)
                .ToListAsync();
        }

        public async Task<long> AddAsync(T entity)
        {
            if (entity is BaseEntity baseEntity)
            {
                baseEntity.CreatedAt = DateTime.UtcNow;
                baseEntity.UpdatedAt = DateTime.UtcNow;
                baseEntity.Status = 1; // Active
                baseEntity.RevSeq = 1; // Initial revision
            }

            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

            // Assuming BaseEntity has an Id property of type long
            return (entity as BaseEntity)?.RowId ?? 0;
        }


        public async Task UpdateAsync(T entity)
        {
            if (entity is BaseEntity baseEntity)
            {
                baseEntity.UpdatedAt = DateTime.UtcNow;
                baseEntity.RevSeq = (short)(baseEntity.RevSeq + 1);
            }
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                entity.Status = 0; // Soft delete
                entity.UpdatedAt = DateTime.UtcNow;
                entity.RevSeq = (short)(entity.RevSeq + 1);
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
            }
            if (entity == null)
                throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with Id {id} was not found.");
        }


        public async Task<IEnumerable<T>> QueryAsync(string sql, object? parameters = null)
        {
            return await _connection.QueryAsync<T>(sql, parameters);
        }
    }
}
