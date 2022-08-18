using FluentValidation;

namespace WebDevelopment.Common.Requests.User.Validators;

public class NewUserRequestValidator : AbstractValidator<NewUserRequest>
{
    public NewUserRequestValidator()
    {
        RuleFor(user => (IUserRequest)user).SetValidator(new BaseUserValidator());
    }
}