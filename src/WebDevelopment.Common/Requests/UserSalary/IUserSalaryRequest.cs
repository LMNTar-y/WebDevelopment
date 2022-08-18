using WebDevelopment.Common.Requests.User;

namespace WebDevelopment.Common.Requests.UserSalary;

public interface IUserSalaryRequest
{
    public decimal? Salary { get; set; }

    public NewUserRequest User { get; set; }
}