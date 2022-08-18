using FluentValidation;

namespace WebDevelopment.Common.Requests.UserPosition.Validators;

public class UserPositionWithIdValidator : AbstractValidator<UserPositionWithIdRequest>
{
    public UserPositionWithIdValidator()
    {
        RuleFor(u => u).SetValidator(new BaseUserPositionValidator());
        RuleFor(u => u.Id).NotNull();
        RuleFor(u => u.EndDate).GreaterThanOrEqualTo(u => u.StartDate);
    }
}