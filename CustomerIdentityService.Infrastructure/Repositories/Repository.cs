using CustomerIdentityService.Core.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomerIdentityService.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbContext _context;
        public Repository(DbContext _context)
        {
            this._context = _context;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }
        public void Update(T entity)
        {
           _context.Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _context.UpdateRange(entities);
        }
        public async Task<T> GetById(object Id)
        {
            var entity = await _context.Set<T>().FindAsync(Id);
            if (entity == null) return null;
            return entity;
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(predicate);
            if (entity == null) return null;
            return entity;
        }

        public async Task<IEnumerable<T>> ToListAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> ToList(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public IQueryable<T> ListIncludes(params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable();
            return includes.Aggregate(query, (q, w) => q.Include(w));
        }
        public IQueryable<T> GetAll(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true, bool ignoreQueryFilters = false)
        {
            var query = _context.Set<T>().AsQueryable();

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (ignoreQueryFilters)
            {
                query = query.IgnoreQueryFilters();
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }

        public void Detached(T entity)
        {

            _context.Entry(entity).State = EntityState.Detached;
        }
    }
}
