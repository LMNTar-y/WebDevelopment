namespace WebDevelopment.API.Model;

public class UpdateUserRequest : IUserRequest
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
}