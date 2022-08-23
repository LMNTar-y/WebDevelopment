using System.Net.Mail;

namespace WebDevelopment.Email.Settings
{
    public class SmtpClientSendMailAsyncWrapper
    {

        public virtual async Task SendAsync(SmtpClient smtpClient, MailMessage message)
        {
            await smtpClient.SendMailAsync(message);
        }
    }
}
