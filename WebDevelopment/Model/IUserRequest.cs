namespace WebDevelopment.API.Model;

public interface IUserRequest
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
}