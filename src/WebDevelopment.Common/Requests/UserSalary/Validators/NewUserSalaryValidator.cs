using FluentValidation;

namespace WebDevelopment.Common.Requests.UserSalary.Validators;

public class NewUserSalaryValidator : AbstractValidator<NewUserSalaryRequest>
{
    public NewUserSalaryValidator()
    {
        RuleFor(u => u).SetValidator(new BaseUserSalaryValidator());
    }
}