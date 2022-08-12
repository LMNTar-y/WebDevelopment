using FluentValidation;

namespace WebDevelopment.Common.Requests.UserSalary.Validators;

public class BaseUserSalaryValidator : AbstractValidator<IUserSalaryRequest>
{
    public BaseUserSalaryValidator()
    {
        RuleFor(u => u.User).NotNull();
        RuleFor(u => u.Salary).NotEmpty();
    }
}