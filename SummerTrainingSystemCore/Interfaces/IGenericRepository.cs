using System.Collections.Generic;
using System.Threading.Tasks;

namespace SummerTrainingSystemCore.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
