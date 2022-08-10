using FluentValidation;

namespace WebDevelopment.Common.Requests.Country.Validators;

public class CountryWithIdValidator : AbstractValidator<CountryWithIdRequest>
{
    public CountryWithIdValidator()
    {
        RuleFor(country => (ICountryRequest)country).SetValidator(new BaseCountryValidator());
        RuleFor(country => country.Id).NotNull().WithMessage("Please ensure you have entered your {PropertyName}");
    }
}