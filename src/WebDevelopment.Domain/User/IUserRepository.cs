
using WebDevelopment.Common.Requests.User;

namespace WebDevelopment.Domain.User
{
    public interface IUserRepository : IDefaultRepository<UserWithIdRequest, NewUserRequest>
    {
    }
}
