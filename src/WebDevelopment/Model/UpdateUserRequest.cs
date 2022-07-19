
namespace WebDevelopment.API.Model;

public class UpdateUserRequest : IUserRequest
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? SecondName { get; set; }

    public string? UserEmail { get; set; }
}