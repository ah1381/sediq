using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Raya.Hrm.Shared.Library.Models;
using Raya.Hrm.Shared.Library.Models.Exception;
using Raya.Hrm.Shared.Library.ModelS;
using Raya.Hrm.Shared.Library.Utilities;
using System.Data;
using System.Linq.Dynamic.Core;
using System.Security.Cryptography;
using System.Text;

namespace Raya.Hrm.Shared.Library.GeneralRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly DbContext _context;
        private readonly IDbConnection _connection;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private string username = string.Empty;

        public GenericRepository(DbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _connection = context.Database.GetDbConnection();
            _httpContextAccessor = httpContextAccessor;
            username = _httpContextAccessor.HttpContext?.User?.FindFirst("Username")?.Value ?? "System";
        }
        public static string GenerateRandId(int bais, int randLength, string separator)
        {
            const string base36Chars = "0123456789abcdefghijklmnopqrstuvwxyz";

            // 1. Get number similar to PostgreSQL: epoch * 100000 + bais
            long epochMicro = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() * 100;
            long num = epochMicro + bais;

            // 2. Convert number to base36
            var sbBase36 = new StringBuilder();
            long tmp = num;
            if (tmp == 0) sbBase36.Append("0");
            while (tmp > 0)
            {
                int remainder = (int)(tmp % 36);
                sbBase36.Insert(0, base36Chars[remainder]);
                tmp /= 36;
            }

            // 3. Generate random part (md5 of random + timestamp)
            using var md5 = MD5.Create();
            string seed = $"{Random.Shared.Next()}_{DateTime.UtcNow:O}";
            byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(seed));

            var sbHex = new StringBuilder();
            foreach (var b in hash)
                sbHex.Append(b.ToString("x2"));

            string randomPart = sbHex.ToString().Substring(0, randLength);

            // 4. Return combined
            return $"{sbBase36}{separator}{randomPart}".ToLower();
        }

        public virtual async Task<T?> GetByIdAsync(long id)
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

        public async Task<T> AddAsync(T entity)
        {
            if (entity is BaseEntity baseEntity)
            {
                baseEntity.CreatedAt = DateTime.UtcNow;
                baseEntity.UpdatedAt = DateTime.UtcNow;
                baseEntity.Status = 1; // Active
                baseEntity.RevSeq = 1; // Initial revision
                baseEntity.CreatedBy = username;

                // Generate RandId in C# instead of SQL
                baseEntity.RandId = GenerateRandId(1000, 6, "-");
            }

            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
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

        public async Task<List<T>> AddListAsync(List<T> entities)
        {
            var result = new List<T>();
            foreach (var entity in entities)
            {
                result.Add(await AddAsync(entity));
            }
            return result;
        }

        public IQueryable<T> GetQueryable()
        {
            return _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetFilteredAsync(FilterPagedListParameter<T>? filters)
        {
            var query = _context.Set<T>().Where(e => e.Status != 0);

            if (filters?.IsHasFilter == true && !string.IsNullOrEmpty(filters.Filter))
            {
                filters.Validate(); // Throws if disallowed props used
                try
                {
                    query = query.Where(filters.Filter);
                }
                catch (Exception ex)
                {
                    throw new BpcValidationException(
                        new List<ValidationError> { new ValidationError("Filter", $"Invalid filter expression: {ex.Message}") });
                }
            }

            // Store total count before paging
            int totalCount = await query.CountAsync();

            if (filters?.IsHasOrderBy == true && !string.IsNullOrEmpty(filters.OrderBy))
            {
                query = query.OrderBy(filters.OrderBy);
            }

            // Apply paging
            if (filters.IsNotNull() && filters.PageIndex > 0)
            {
                query = query.Skip((filters.PageIndex - 1) * filters.PageSize).Take(filters.PageSize);
            }

            return await query.ToListAsync();
        }

    }
}
