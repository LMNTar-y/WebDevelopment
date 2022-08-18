using FluentValidation;

namespace WebDevelopment.Common.Requests.UserPosition.Validators;

public class NewUserPositionValidator : AbstractValidator<NewUserPositionRequest>
{
    public NewUserPositionValidator()
    {
        RuleFor(u => u).SetValidator(new BaseUserPositionValidator());
    }
}