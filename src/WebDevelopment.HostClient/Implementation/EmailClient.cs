using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using WebDevelopment.HostClient.Interfaces;
using WebDevelopment.HostClient.Model;

namespace WebDevelopment.HostClient.Implementation;

public class EmailClient : ISenderClient
{
    private readonly SmtpClientSetups _configurations;
    private SmtpClient? _smtpClient;

    public EmailClient(IOptions<SmtpClientSetups> configurations)
    {
        _configurations = configurations.Value;
    }

    public void SendNotification(List<string> emailsToSend)
    {
        try
        {
            if (!(emailsToSend?.Count > 0) || _configurations?.EmailSendFrom == null ||
                _configurations.EmailPassword == null || _configurations.EncryptionKey == null) return;

            using (_smtpClient = new SmtpClient(_configurations.SmtpHost, _configurations.SmtpPort))
            {
                _smtpClient.Credentials = new NetworkCredential(_configurations.EmailLogin,
                    EmailPasswordDecryptor.DecryptCipherTextToPlainText(_configurations.EmailPassword,
                        _configurations.EncryptionKey));
                _smtpClient.EnableSsl = true;

                var from = new MailAddress(_configurations.EmailSendFrom, "WebDevelopment");

                foreach (var email in emailsToSend)
                {
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
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}