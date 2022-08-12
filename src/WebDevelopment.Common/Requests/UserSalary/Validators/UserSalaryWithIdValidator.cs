using FluentValidation;

namespace WebDevelopment.Common.Requests.UserSalary.Validators;

public class UserSalaryWithIdValidator : AbstractValidator<UserSalaryWithIdRequest>
{
    public UserSalaryWithIdValidator()
    {
        RuleFor(u => u).SetValidator(new BaseUserSalaryValidator());
        RuleFor(u => u.Id).NotEmpty();
    }
}