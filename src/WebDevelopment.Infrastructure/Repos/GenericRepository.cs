using Microsoft.EntityFrameworkCore;
using WebDevelopment.Domain.IRepos;

namespace WebDevelopment.Infrastructure.Repos;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly WebDevelopmentContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(WebDevelopmentContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(string includeProperties = "")
    {
        IQueryable<TEntity> query = _dbSet;

        foreach (var includeProperty in includeProperties.Split
                     (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty.Trim());
        }

        var result = await query.ToListAsync();
        return result;
    }

    public async Task<TEntity> GetByIdAsync(object id)
    {
        return await _dbSet.FindAsync(id) ??
               throw new ArgumentNullException(nameof(id), $"RowData with id {id} was not fount in the DB");
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}