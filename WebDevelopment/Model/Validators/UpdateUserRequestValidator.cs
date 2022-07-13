using FluentValidation;

namespace WebDevelopment.API.Model.Validators;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(user => (IUserRequest)user).SetValidator(new BaseUserValidator());
        RuleFor(user => user.Id).NotNull().WithMessage("Please ensure you have entered your {PropertyName}");
    }
}