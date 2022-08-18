using FluentValidation;

namespace WebDevelopment.Common.Requests.UserPosition.Validators;

public class BaseUserPositionValidator : AbstractValidator<IUserPositionRequest>
{
    public BaseUserPositionValidator()
    {
        RuleFor(u => u.User).NotNull();
        RuleFor(u => u.Department).NotNull();
        RuleFor(u => u.Position).NotNull();
    }
}