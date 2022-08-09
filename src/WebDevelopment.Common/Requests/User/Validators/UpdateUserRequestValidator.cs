using FluentValidation;

namespace WebDevelopment.Common.Requests.User.Validators;

public class UpdateUserRequestValidator : AbstractValidator<UserWithIdRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(user => (IUserRequest)user).SetValidator(new BaseUserValidator());
        RuleFor(user => user.Id).NotNull().WithMessage("Please ensure you have entered your {PropertyName}");
    }
}