
using WebDevelopment.Common.Requests.User;

namespace WebDevelopment.Domain.User
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserWithIdRequest>> GetAll();
        Task<NewUserRequest> Add(NewUserRequest user);
        Task<UserWithIdRequest> Update(UserWithIdRequest userWithId);
    }
}
