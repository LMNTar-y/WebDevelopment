using FluentValidation;

namespace WebDevelopment.Email.Model.Validators
{
    public class EmailProviderSettingsValidator : AbstractValidator<EmailProviderSettings>
    {
        public EmailProviderSettingsValidator()
        {
            RuleFor(settings => settings.SmtpHost).NotEmpty().Length(5, 100)
                .WithMessage("{PropertyName} should not be null or empty");
            RuleFor(settings => settings.SmtpPort).NotNull().InclusiveBetween(0, 999)
                .WithMessage("{PropertyName} should be more than 0 and in the adequate range");
            RuleFor(settings => settings.EmailSendFrom).EmailAddress().WithMessage("Please check if you wrote a correct {PropertyName}");
            RuleFor(settings => settings.EmailPassword).NotEmpty().NotEqual(s => s.EmailSendFrom)
                .WithMessage("Please check if you wrote a correct {PropertyName}");
        }
    }
}
