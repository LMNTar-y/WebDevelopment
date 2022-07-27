using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebDevelopment.Email.Model;
using WebDevelopment.Email.Security;

namespace WebDevelopment.Email.Settings;

public abstract class EmailSmtpClientHelper : IDisposable
{
    protected MailAddress From;
    private SmtpClient _smtpClient = null!;
    private readonly ILogger<EmailSmtpClientHelper> _logger;
    private readonly EmailProviderSettings _emailProviderSettings;


    protected EmailSmtpClientHelper(IServiceProvider serviceProvider, EmailProviderName emailProviderName)
    {
        _logger = serviceProvider.GetRequiredService<ILogger<EmailSmtpClientHelper>>();
        _emailProviderSettings = serviceProvider.GetRequiredService<IConfiguration>()
            .GetSection($"{nameof(EmailSettings)}:{nameof(EmailProviderSettings)}:{emailProviderName}")
            .Get<EmailProviderSettings>() ?? throw new ArgumentNullException(nameof(serviceProvider),
            $"Error with receiving settings for {_emailProviderSettings} from the appsettings file");
        From = new MailAddress(_emailProviderSettings.EmailSendFrom, "WebDevelopment");
    }

    public async Task<bool> SendEmailAsync(MailMessage message) //message
    {
        var retValue = false;

        try
        {
            //using
            using (_smtpClient = new SmtpClient(_emailProviderSettings.SmtpHost, _emailProviderSettings.SmtpPort))
            {
                _smtpClient.Credentials = new NetworkCredential(_emailProviderSettings.EmailLogin,
                    EmailPasswordDecryptor.DecryptCipherTextToPlainText(_emailProviderSettings.EmailPassword,
                        EmailPasswordDecryptor.EncryptKey));
                _smtpClient.EnableSsl = true;

                await _smtpClient.SendMailAsync(message);
            }

            retValue = true;
            Dispose();
        }
        catch (Exception ex)
        {
            _logger.LogCritical($"EmailSmtpClientHelper - SendEmailAsync - {ex.Message}");
        }

        return retValue;
    }

    public void Dispose()
    {
        _smtpClient?.Dispose();
    }
}