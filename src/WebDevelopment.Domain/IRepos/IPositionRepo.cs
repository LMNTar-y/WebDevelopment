using WebDevelopment.Common.Requests.Position;

namespace WebDevelopment.Domain.IRepos;

public interface IPositionRepo : IGenericRepository<IPositionRequest>
{
    Task<IPositionRequest> GetByNameAsync(string name);
}