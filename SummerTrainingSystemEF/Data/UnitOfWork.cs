using SummerTrainingSystemCore.Entities;
using SummerTrainingSystemCore.Interfaces;
using SummerTrainingSystemEF.Data.Repositories;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace SummerTrainingSystemEF.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private Hashtable _repositories;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Hashtable();

            var typeName = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(typeName))
            {
                var repoType = typeof(GenericRepository<>);
                var repoInstance = Activator.CreateInstance(repoType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(typeName, repoInstance);
            }

            return (IGenericRepository<TEntity>)_repositories[typeName];
        }

        public ITrainingRepository TrainningRepository()
        {
            if (_repositories == null) _repositories = new Hashtable();
            var typeName = typeof(Trainning).Name;
            if (!_repositories.ContainsKey(typeName))
            {
                var repoType = typeof(TrainingRepository);
                var repoInstance = Activator.CreateInstance(repoType, _context);
                _repositories.Add(typeName, repoInstance);
            }
            return (ITrainingRepository)_repositories[typeName];
        }
    }
}
