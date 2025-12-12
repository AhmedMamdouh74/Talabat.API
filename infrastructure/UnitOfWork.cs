using Domain;
using Domain.Contracts;
using infrastructure.Repos;
using System.Collections.Concurrent;

namespace infrastructure
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext dbContext;
        private ConcurrentDictionary<string, object> repositories = new ConcurrentDictionary<string, object>();



        public UnitOfWork(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name;
            if (!repositories.ContainsKey(type))
            {
                var repositoryInstance = new GenericRepository<TEntity>(dbContext);
                repositories.TryAdd(type, repositoryInstance);
            }
            return (IGenericRepository<TEntity>)repositories[type];
        }

        public Task<int> CompleteAsync()
        {
            return dbContext.SaveChangesAsync();
        }

       

        public async ValueTask DisposeAsync()
        {
           await dbContext.DisposeAsync();
        }
    }
}
