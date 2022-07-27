using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebDevelopment.Email.Model;

namespace WebDevelopment.Email.Settings;

public class MaiLMassageSettings
{
    private readonly IServiceProvider _serviceProvider;
    public MaiLMassageSettings(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public MailMessage CreateMessage(MailAddress from, string to)
    {
        var emailSettings = _serviceProvider.GetRequiredService<IConfiguration>().GetSection(nameof(EmailSettings))
            .Get<EmailSettings>();

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