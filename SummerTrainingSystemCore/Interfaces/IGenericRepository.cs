using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SummerTrainingSystemCore.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        // get entity by id with out related entities
        Task<T> GetByIdAsync(int id);

        // get entity by any criteria with related entities
        Task<T> GetAsync(Expression<Func<T, bool>> criteria, string[] includes);

        // get all entities with out related entities
        Task<IReadOnlyList<T>> ListAllAsync();

        // get all entities with criteria with related entities
        Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> criteria, string[] includes);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
