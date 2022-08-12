namespace WebDevelopment.Domain;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAll();

    Task<TEntity> Add(TEntity entity);

   Task<TEntity> Update(TEntity entity);
}