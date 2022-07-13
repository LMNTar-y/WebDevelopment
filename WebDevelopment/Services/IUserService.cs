using WebDevelopment.API.Model;

namespace WebDevelopment.API.Services
{
    public interface IUserService
    {
        IEnumerable<NewUserRequest> GetAllUsers();
        NewUserRequest GetUserById(int id);
        NewUserRequest GetUserByEmail(string userEmail);
        Task CreateNewUserAsync(NewUserRequest userRequest);
        Task UpdateNewUserAsync(UpdateUserRequest userRequest);
    }
}
