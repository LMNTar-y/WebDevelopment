using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using WebDevelopment.HostClient.Security;

namespace WebDevelopment.HostClient.Model;

public class SmtpClientSetupsFactory
{
    private readonly IConfiguration _configuration;
    public string EmailSendFrom { get; set; } = null!;

    public SmtpClientSetupsFactory(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration),
            $"{configuration} was not loaded from DI");
    }

    public SmtpClient Create(EmailProvider? emailProvider)
    {
        var emailProviderSettings = _configuration.GetSection($"EmailSettings:EmailProviderSettings:{emailProvider}")
            .Get<EmailProviderSettings>();

        if (string.IsNullOrWhiteSpace(emailProviderSettings?.EmailPassword) ||
            string.IsNullOrWhiteSpace(emailProviderSettings.SmtpHost) ||
            string.IsNullOrWhiteSpace(emailProviderSettings.EmailLogin) ||
            string.IsNullOrWhiteSpace(emailProviderSettings.EmailSendFrom))
            throw new ArgumentNullException(nameof(emailProvider), $"EmailProviderSettings incorrect or was not found in the appsettings for {emailProvider}");

        var smtp = new SmtpClient(emailProviderSettings.SmtpHost, emailProviderSettings.SmtpPort);
        smtp.Credentials = new NetworkCredential(emailProviderSettings.EmailLogin,
            EmailPasswordDecryptor.DecryptCipherTextToPlainText(emailProviderSettings.EmailPassword,
                EmailPasswordDecryptor.EncryptKey));
        smtp.EnableSsl = true;

        EmailSendFrom = emailProviderSettings.EmailSendFrom;

        return smtp;
    }
}