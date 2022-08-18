using WebDevelopment.Common.Requests.LoginModel;

namespace WebDevelopment.API.Services;

public interface ILoginService
{
    Task<string> LoginAsync(UserLogin userLogin);
    Task<bool> RegisterAsync(NewUserLogin userLogin);
}