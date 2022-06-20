using Microsoft.EntityFrameworkCore.Query;
using SummerTrainingSystemCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Tests.TestClasses
{
    public class FakeIGenericRepository<T> : IGenericRepository<T> where T : class
    {
        private T fakeInstance;

        public FakeIGenericRepository(T instance = null) { fakeInstance = instance; }
        public void Add(T entity)
        {

        }

        public void Delete(T entity)
        {

        }

        // return one entity with related data
        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> criteria, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return fakeInstance;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return fakeInstance;
        }

        public async Task<T> GetByStringIdAsync(string id)
        {
            return fakeInstance;
        }

        // return all entities without related data
        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return new List<T>();
        }

        // return entities with related data
        public virtual async Task<IReadOnlyList<T>> ListAsync(
            Expression<Func<T, bool>> criteria,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
            )
        {
            return new List<T>();
        }

        public virtual void Update(T entity)
        {

        }
    }
}
