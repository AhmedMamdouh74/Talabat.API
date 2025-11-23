using Domain;
using Domain.Concrats;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Repos
{
    public class GenericRepository<T, TK> : IGenericRepository<T, TK> where T : BaseEntity<TK>
    {
        private readonly AppDbContext dbContext;

        public GenericRepository(AppDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))

                return (IReadOnlyList<T>)await dbContext.Set<Product>()
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .ToListAsync();


            return await dbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(TK id)
        {
            if (typeof(T) == typeof(Product)) return await dbContext.Set<Product>()
                .Include(p => p.Category)
                .Include(p => p.Brand).FirstOrDefaultAsync(p => p.Id.Equals(id)) as T;
            ;
            return await dbContext.Set<T>().FindAsync(id);
        }
    }
}
