using Microsoft.EntityFrameworkCore;
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

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public int Add(T entity)
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChanges();
        }

        public int Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return _context.SaveChanges();
        }

        // return one entity with related data
        public async Task<T> GetAsync(Expression<Func<T, bool>> criteria, string[] includes)
        {
            var query = _context.Set<T>().AsQueryable<T>();
            if(includes != null)
            {
                foreach(var inc in includes)
                {
                    query = query.Include(inc);
                }
            }
            return await query.SingleOrDefaultAsync(criteria);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        // return all entities without related data
        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        // return entities with related data
        public async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> criteria, string[] includes)
        {
            var query = _context.Set<T>().AsQueryable<T>();
            query = query.Where(criteria);
            if (includes != null)
            {
                foreach (var inc in includes)
                {
                    query = query.Include(inc);
                }
            }
            return await query.ToListAsync();
        }

        public int Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return _context.SaveChanges();
        }
    }
}
