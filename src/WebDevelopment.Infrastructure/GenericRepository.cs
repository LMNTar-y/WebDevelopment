using Microsoft.EntityFrameworkCore;
using WebDevelopment.Domain;

namespace WebDevelopment.Infrastructure
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class 
    {
        private readonly WebDevelopmentContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(WebDevelopmentContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var result = await _dbSet.ToListAsync();
            return result;
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

    }
}
