using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SummerTrainingSystemCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SummerTrainingSystemEF.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public int Add(T entity)
        {
            _dbSet.Add(entity);
            return _context.SaveChanges();
        }

        public int Delete(T entity)
        {
            _dbSet.Remove(entity);
            return _context.SaveChanges();
        }

        // return one entity with related data
        public async Task<T> GetAsync(Expression<Func<T, bool>> criteria, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var query = _dbSet.AsQueryable<T>();
            if (include != null)
            {
                query = include(query);
            }
            return await query.SingleOrDefaultAsync(criteria);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        // return all entities without related data
        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // return entities with related data
        public async Task<IReadOnlyList<T>> ListAsync(
            Expression<Func<T, bool>> criteria, 
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
            )
        {
            var query = _dbSet.AsQueryable<T>();
            query = query.Where(criteria);
            if (include != null)
            {
                query = include(query);
            }

            return await query.ToListAsync();
        }

        public int Update(T entity)
        {
            _dbSet.Update(entity);
            return _context.SaveChanges();
        }
    }
}
