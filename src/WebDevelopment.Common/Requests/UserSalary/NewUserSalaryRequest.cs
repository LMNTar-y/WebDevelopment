using WebDevelopment.Common.Requests.User;

namespace WebDevelopment.Common.Requests.UserSalary;

public class NewUserSalaryRequest : IUserSalaryRequest
{
    public decimal? Salary { get; set; }
    public NewUserRequest User { get; set; } = null!;
}