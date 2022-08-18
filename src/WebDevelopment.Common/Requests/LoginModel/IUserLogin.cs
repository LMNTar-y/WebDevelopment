namespace WebDevelopment.Common.Requests.LoginModel;

public interface IUserLogin
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
}