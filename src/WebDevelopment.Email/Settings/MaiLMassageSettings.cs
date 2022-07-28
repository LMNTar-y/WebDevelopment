using System.Net.Mail;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebDevelopment.Email.Model;
using WebDevelopment.Email.Model.Validators;

namespace WebDevelopment.Email.Settings;

public class MailMassageSettings
{
    private readonly IServiceProvider _serviceProvider;
    public MailMassageSettings(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<MailMessage> CreateMessageAsync(MailAddress from, string to)
    {
        var emailSettings = _serviceProvider.GetRequiredService<IConfiguration>().GetSection(nameof(EmailSettings))
            .Get<EmailSettings>();
        var emailSettingsValidator = new EmailSettingsValidator();
        await emailSettingsValidator.ValidateAndThrowAsync(emailSettings);

        var message = new MailMessage();
        message.From = from;
        message.To.Add(to);
        message.Subject = emailSettings.EmailSubject;
        message.SubjectEncoding = System.Text.Encoding.UTF8;
        message.Body = emailSettings.EmailBody;
        message.BodyEncoding = System.Text.Encoding.UTF8;
        return message;
    }
}