using FluentValidation;

namespace WebDevelopment.Common.Requests.SalaryRange.Validators;

public class BaseSalaryRangeValidator : AbstractValidator<ISalaryRangeRequest>
{
    public BaseSalaryRangeValidator()
    {
        RuleFor(s => s.StartRange).NotEmpty().WithMessage("{PropertyName} should not be null or default");
        RuleFor(s => s.FinishRange).NotEmpty().GreaterThanOrEqualTo(s => s.StartRange)
            .WithMessage("{PropertyName} should not be null or default or less than StartRange");
    }
}