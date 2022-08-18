using FluentValidation;

namespace WebDevelopment.Common.Requests.User.Validators;

public class UserWithIdRequestValidator : AbstractValidator<UserWithIdRequest>
{
    public UserWithIdRequestValidator()
    {
        RuleFor(user => (IUserRequest)user).SetValidator(new BaseUserValidator());
        RuleFor(user => user.Id).NotNull().WithMessage("Please ensure you have entered your {PropertyName}");
    }
}