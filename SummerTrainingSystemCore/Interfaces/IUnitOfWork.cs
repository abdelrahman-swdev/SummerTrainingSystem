using System;
using System.Threading.Tasks;

namespace SummerTrainingSystemCore.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : class;
        ITrainingRepository TrainningRepository();
        Task<int> Complete();
    }
}
