using FluentValidation;

namespace WebDevelopment.Email.Model.Validators;

public class EmailSettingsValidator : AbstractValidator<EmailSettings>
{
    public EmailSettingsValidator()
    {
        RuleFor(s => s.EmailSubject).NotEmpty().WithMessage("{PropertyName} should not be null or empty");
        RuleFor(s => s.EmailBody).NotEmpty().WithMessage("{PropertyName} should not be null or empty");
    }
}