namespace WebDevelopment.Common.Requests.LoginModel;

public class NewUserLogin : IUserLogin
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? UserEmail { get; set; }
}