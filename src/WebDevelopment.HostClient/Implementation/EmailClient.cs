using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using WebDevelopment.HostClient.Interfaces;
using WebDevelopment.HostClient.Model;

namespace WebDevelopment.HostClient.Implementation;

public class EmailClient : ISenderClient
{
    private readonly SmtpClientSetups _configurations;
    private readonly ILogger<EmailClient> _logger;
    private SmtpClient? _smtpClient;
    const string EmailPattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                               @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

    public EmailClient(IOptions<SmtpClientSetups> configurations, ILogger<EmailClient> logger)
    {
        _logger = logger;
        if (configurations == null) throw new ArgumentNullException(nameof(configurations), $"{configurations} was not loaded from DI");
        _configurations = configurations.Value;
    }

    public void SendNotification(List<string> emailsToSend)
    {
        try
        {
            if (emailsToSend == null || emailsToSend.Count < 1)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(_configurations?.EmailSendFrom) ||
                string.IsNullOrWhiteSpace(_configurations.EmailPassword) || string.IsNullOrWhiteSpace(_configurations.EncryptionKey) || _configurations.SmtpPort < 0)
            {
                throw new ArgumentException("Incorrect data in the SmtpClientSetups configurations");
            }

            using (_smtpClient = new SmtpClient(_configurations.SmtpHost, _configurations.SmtpPort))
            {
                _smtpClient.Credentials = new NetworkCredential(_configurations.EmailLogin,
                    EmailPasswordDecryptor.DecryptCipherTextToPlainText(_configurations.EmailPassword,
                        _configurations.EncryptionKey));
                _smtpClient.EnableSsl = true;

                var from = new MailAddress(_configurations.EmailSendFrom, "WebDevelopment");

                foreach (var email in emailsToSend)
                {
                    if (!Regex.IsMatch(email, EmailPattern, RegexOptions.IgnoreCase))
                    {
                        _logger.LogWarning("Incorrect email in dataBase - {0}", email);
                        continue;
                    }

                    var to = new MailAddress(email);
                    using (var message = new MailMessage(from, to))
                    {
                        message.Subject = _configurations.EmailSubject;
                        message.SubjectEncoding = System.Text.Encoding.UTF8;
                        message.Body = _configurations.EmailBody;
                        message.BodyEncoding = System.Text.Encoding.UTF8;
                        _smtpClient.Send(message);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "EmailClient - SendNotification - {0}",
                ex.ToString());
            throw;
        }
    }
}