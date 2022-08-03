using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using WebDevelopment.Email.Model;
using Microsoft.Extensions.Logging;
using WebDevelopment.Email.Settings;
using WebDevelopment.Email.Providers.Interfaces;

namespace WebDevelopment.Email.Providers
{
    public class BaseEmailProvider : EmailSmtpClientHelper, IEmailProvider
    {
        private readonly MailMassageSetup _message;
        private readonly ILogger<BaseEmailProvider> _logger;
        const string EmailPattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

        public BaseEmailProvider(IServiceProvider serviceProvider, EmailProviderName emailProviderName) : base(serviceProvider, emailProviderName)
        {
            _message = new MailMassageSetup(serviceProvider);
            _logger = serviceProvider.GetRequiredService<ILogger<BaseEmailProvider>>();
        }

        public async Task<bool> SendNotification(List<string> emailsToSend)
        {
            var retVal = false;

            if (emailsToSend == null || emailsToSend.Count < 1)
            {
                _logger.LogInformation("The List of the emails to sent is null or empty");
                return retVal;
            }
            
            var checkedEmailsToSend = new List<string>();

            try
            {
                foreach (var email in emailsToSend)
                {
                    if (Regex.IsMatch(email, EmailPattern, RegexOptions.IgnoreCase))
                    {
                        checkedEmailsToSend.Add(email);
                    }
                    else
                    {
                        _logger.LogWarning("Incorrect email in the dataBase - {0}", email);
                    }
                }

                if (checkedEmailsToSend.Count < 1)
                {
                    _logger.LogInformation("The List of the checked emails to sent is null or empty");
                    return retVal;
                }

                var joined = string.Join(",", checkedEmailsToSend);

                using var message = await _message.CreateMessageAsync(From, joined);
                await SendEmailAsync(message);

                retVal = true;
                message?.Dispose();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"BaseEmailProvider - SendNotification - {ex.Message}");
            }

            return retVal;
        }

    }
}

