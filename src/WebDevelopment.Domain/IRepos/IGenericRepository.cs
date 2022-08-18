namespace WebDevelopment.Domain.IRepos;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync(string includeProperties = "");

    Task<TEntity> GetByIdAsync(object id);

    Task<TEntity> AddAsync(TEntity entity);

    Task<TEntity> UpdateAsync(TEntity entity);
}