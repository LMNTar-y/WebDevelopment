using FluentValidation;

namespace WebDevelopment.Common.Requests.Country.Validators;

public class NewCountryValidator : AbstractValidator<NewCountryRequest>
{
    public NewCountryValidator()
    {
        RuleFor(country => (ICountryRequest)country).SetValidator(new BaseCountryValidator());
    }
}