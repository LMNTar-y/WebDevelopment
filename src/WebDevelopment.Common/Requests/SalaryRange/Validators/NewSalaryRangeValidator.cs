using FluentValidation;

namespace WebDevelopment.Common.Requests.SalaryRange.Validators;

public class NewSalaryRangeValidator : AbstractValidator<NewSalaryRangeRequest>
{
    public NewSalaryRangeValidator()
    {
        RuleFor(s => s).SetValidator(new BaseSalaryRangeValidator());
    }
}