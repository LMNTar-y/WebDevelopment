
namespace WebDevelopment.Common.Requests.User;

public class UserWithIdRequest : IUserRequest
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? SecondName { get; set; }

    public string? UserEmail { get; set; }

    public bool? Active { get; set; }
}