
using FluentValidation;

namespace WebDevelopment.Common.Requests.Position.Validators
{
    public class BasePositionValidator : AbstractValidator<IPositionRequest>
    {
        public BasePositionValidator()
        {
            RuleFor(p => p.Name).NotEmpty().Length(5, 20).WithMessage("{PropertyName} should be more than 1 letter and less than 20");
            RuleFor(p => p.ShortName).NotEmpty().Length(2, 10).WithMessage("{PropertyName} should be more than 1 letter and less than 10");
        }
    }
}
