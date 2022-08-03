using System.Net;
using System.Net.Mail;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebDevelopment.Email.Model;
using WebDevelopment.Email.Model.Validators;
using WebDevelopment.Email.Security;

namespace WebDevelopment.Email.Settings;

public abstract class EmailSmtpClientHelper : IDisposable
{
    protected MailAddress From;
    private SmtpClient _smtpClient = null!;
    public SmtpClientSendMailAsyncWrapper SmtpClientSendMailAsyncWrapper { get; set; }
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
        SmtpClientSendMailAsyncWrapper = new SmtpClientSendMailAsyncWrapper();
    }

    public async Task<bool> SendEmailAsync(MailMessage message) //message
    {
        _logger.LogInformation("EmailSmtpClientHelper - SendEmailAsync - method started");
        var retValue = false;

        if (message?.From == null || message?.Bcc == null)
        {
            _logger.LogCritical($"EmailSmtpClientHelper - SendEmailAsync - {message} is not created or empty");
            return retValue;
        }

        try
        {
            _logger.LogInformation("Validation of email provider settings started");
            var providerSettingsValidator = new EmailProviderSettingsValidator();
            await providerSettingsValidator.ValidateAndThrowAsync(_emailProviderSettings);
            _logger.LogInformation("Validation of email provider settings finished");

            using (_smtpClient = new SmtpClient(_emailProviderSettings.SmtpHost, _emailProviderSettings.SmtpPort))
            {
                _smtpClient.Credentials = new NetworkCredential(_emailProviderSettings.EmailLogin,
                    EmailPasswordDecryptor.DecryptCipherTextToPlainText(_emailProviderSettings.EmailPassword,
                        EmailPasswordDecryptor.EncryptKey));
                _smtpClient.EnableSsl = true;

                //await _smtpClient.SendMailAsync(message);
                await SmtpClientSendMailAsyncWrapper.SendAsync(_smtpClient, message);
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