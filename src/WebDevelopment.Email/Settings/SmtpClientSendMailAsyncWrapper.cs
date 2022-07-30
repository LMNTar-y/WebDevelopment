using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

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
