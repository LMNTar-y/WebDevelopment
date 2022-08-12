using WebDevelopment.Common.Requests.User;

namespace WebDevelopment.Common.Requests.UserSalary;

public class UserSalaryWithIdRequest : IUserSalaryRequest
{
    public int Id { get; set; }
    public decimal? Salary { get; set; }
    public NewUserRequest User { get; set; } = null!;
    public DateTimeOffset? ChangeTime { get; set; }
}