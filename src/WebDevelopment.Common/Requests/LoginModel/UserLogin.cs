namespace WebDevelopment.Common.Requests.LoginModel;

public class UserLogin : IUserLogin
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
}