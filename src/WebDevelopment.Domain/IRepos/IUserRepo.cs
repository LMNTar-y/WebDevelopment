using WebDevelopment.Common.Requests.User;

namespace WebDevelopment.Domain.IRepos;

public interface IUserRepo : IGenericRepository<IUserRequest>
{
    Task<IUserRequest> GetUserByEmail(string email);
}