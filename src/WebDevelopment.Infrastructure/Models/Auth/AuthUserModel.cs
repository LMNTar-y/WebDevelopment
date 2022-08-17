namespace WebDevelopment.Infrastructure.Models.Auth;

public class AuthUserModel
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public Roles Role { get; set; }
    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}