using Microsoft.Extensions.Logging;
using WebDevelopment.Common.Requests.User;

namespace WebDevelopment.Domain.User.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository, ITestUserRepo userRepo)
    {
        _userRepository = userRepository ?? throw new ArgumentException($"{nameof(userRepository)} was not downloaded from DI");
    }

    public async Task<IEnumerable<UserWithIdRequest>> GetAllUsers()
    {
        var result = await _userRepository.GetAll();
        return result;
    }

    public async Task<UserWithIdRequest> GetUserById(int id)
    {
        var result = (await _userRepository.GetAll()).FirstOrDefault(x => x.Id == id) ??
                     throw new ArgumentNullException(nameof(id), $"User with {id} has not fount in the DataBase");

        return result;
    }

    public async Task<UserWithIdRequest> GetUserByEmail(string userEmail)
    {
        var result = (await _userRepository.GetAll()).FirstOrDefault(x => string.Equals(x.UserEmail, userEmail, StringComparison.CurrentCultureIgnoreCase)) ??
                     throw new ArgumentNullException(nameof(userEmail), $"User with {userEmail} has not fount in the DataBase");

        return result;
    }

    public async Task<bool> CreateNewUserAsync(NewUserRequest userRequest)
    {
        await _userRepository.Add(userRequest);
        return true;
    }

    public async Task<bool> UpdateUserAsync(UserWithIdRequest userWithIdRequest)
    {
        await _userRepository.Update(userWithIdRequest);
        return true;
    }
}