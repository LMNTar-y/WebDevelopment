using FluentValidation;

namespace WebDevelopment.API.Model.Validators;

public class BaseUserValidator : AbstractValidator<IUserRequest>
{
    public BaseUserValidator()
    {
        RuleFor(user => user.FirstName).NotNull().Length(1, 20)
            .WithMessage("Please ensure you have entered your {PropertyName}");
        RuleFor(user => user.SecondName).NotNull().Length(1, 30)
            .WithMessage("Please ensure you have entered your {PropertyName}");
        RuleFor(user => user.UserEmail).EmailAddress().WithMessage("Please ensure you have entered your {PropertyName}");
    }
}