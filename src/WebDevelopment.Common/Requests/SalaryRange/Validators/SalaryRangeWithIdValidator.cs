using FluentValidation;

namespace WebDevelopment.Common.Requests.SalaryRange.Validators;

public class SalaryRangeWithIdValidator : AbstractValidator<SalaryRangeWithIdRequest>
{
    public SalaryRangeWithIdValidator()
    {
        RuleFor(s => s).SetValidator(new BaseSalaryRangeValidator());
        RuleFor(s => s.Id).NotNull().WithMessage("Please ensure you have entered your {PropertyName}");
    }
}