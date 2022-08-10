
using FluentValidation;

namespace WebDevelopment.Common.Requests.Country.Validators
{
    public class BaseCountryValidator : AbstractValidator<ICountryRequest>
    {
        public BaseCountryValidator()
        {
            RuleFor(c => c.Name).NotEmpty().Length(1, 20).WithMessage("{PropertyName} should be more than 1 letter and less than 20");
            RuleFor(c => c.Alpha3Code).NotEmpty().Length(3).WithMessage("Please ensure you have entered 3 letters {PropertyName}");
        }
    }
}
