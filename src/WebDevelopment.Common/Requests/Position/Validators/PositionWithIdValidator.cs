using FluentValidation;

namespace WebDevelopment.Common.Requests.Position.Validators;

public class PositionWithIdValidator : AbstractValidator<PositionWithIdRequest>
{
    public PositionWithIdValidator()
    {
        RuleFor(p => p).SetValidator(new BasePositionValidator());
        RuleFor(p => p.Id).NotNull().WithMessage("Please ensure you have entered your {PropertyName}");
    }
}