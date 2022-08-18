using FluentValidation;

namespace WebDevelopment.Common.Requests.Position.Validators;

public class NewPositionValidator : AbstractValidator<NewPositionRequest>
{
    public NewPositionValidator()
    {
        RuleFor(pos => pos).SetValidator(new BasePositionValidator());
    }
}