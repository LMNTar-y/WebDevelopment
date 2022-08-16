using WebDevelopment.Common.Requests.UserPosition;

namespace WebDevelopment.Domain.IRepos;

public interface IUserPositionRepo : IGenericRepository<IUserPositionRequest>
{
    Task<IEnumerable<IUserPositionRequest>> GetByUserNameAsync(string firstName, string lastName);
}