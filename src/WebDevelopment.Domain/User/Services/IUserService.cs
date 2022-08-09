using WebDevelopment.Common.Requests.User;

namespace WebDevelopment.Domain.User.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserWithIdRequest>> GetAllUsers();

        Task<UserWithIdRequest> GetUserById(int id);

        Task<UserWithIdRequest> GetUserByEmail(string userEmail);

        Task<bool> CreateNewUserAsync(NewUserRequest userRequest);

        Task<bool> UpdateUserAsync(UserWithIdRequest userWithIdRequest);
    }
}
