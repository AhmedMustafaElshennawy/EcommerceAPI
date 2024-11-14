using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.orderItem;
using Ecommerce.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Common.Interfaces
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext context) => _context = context;

        public async Task<T> CreateEntityAsync(T Entity)
        {
            await _context.Set<T>().AddAsync(Entity);
            return Entity;
        }
        public Task DeleteEntityAsync(T Entity)
        {
            _context.Set<T>().Remove(Entity);
            return Task.CompletedTask;
        }
        public async Task<T> FindAnyAsync(Expression<Func<T, bool>> Match, string[] Includes = null!)
        {
            IQueryable<T> query = _context.Set<T>();
            if (Includes != null)
            {
                foreach (var include in Includes)
                {
                    query = query.Include(include);
                }
            }
            var result = await query.SingleOrDefaultAsync(Match);
            return result!;
        }
        public async Task<T> FindByLamdaAsync(Expression<Func<T, bool>> match, CancellationToken cancellationToken)
        {
            var result = await _context.Set<T>().SingleOrDefaultAsync(match);
            return result!;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await _context.Set<T>().ToListAsync();
            return result;
        }
        public async Task<T> GetEntityByIdAsync(Guid id)
        {
            var result = await _context.Set<T>().FindAsync(id);
            return result!;
        }
        public Task<T> UpdateEntityAsync(T Entity)
        {
            _context.Set<T>().Update(Entity);
            return Task.FromResult(Entity);
        }
        public async Task<IEnumerable<T>> CreateEntitiesAsync(IEnumerable<T> entities)
        {

            await _context.Set<T>().AddRangeAsync(entities);
            return entities;
        }
        // Add a method to access ChangeTracker entries
        public IEnumerable<EntityEntry<T>> GetChangeTrackerEntries() => _context.ChangeTracker.Entries<T>();
        public IQueryable<T> Entities() => _context.Set<T>();
    }
}
