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
        private readonly MaiLMassageSettings _message;
        private readonly ILogger<BaseEmailProvider> _logger;
        const string EmailPattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

        public BaseEmailProvider(IServiceProvider serviceProvider, EmailProviderName emailProviderName) : base(serviceProvider, emailProviderName)
        {
            _message = new MaiLMassageSettings(serviceProvider);
            _logger = serviceProvider.GetRequiredService<ILogger<BaseEmailProvider>>();
        }

        public async Task SendNotification(List<string> emailsToSend)
        {
            if (emailsToSend == null || emailsToSend.Count < 1)
            {
                _logger.LogInformation("The List of the emails to sent is null or empty");
                return;
            }

            foreach (var email in emailsToSend)
            {
                if (!Regex.IsMatch(email, EmailPattern, RegexOptions.IgnoreCase))
                {
                    _logger.LogWarning("Incorrect email in dataBase - {0}", email);
                    emailsToSend.Remove(email);
                }
            }

            var joined = string.Join(",", emailsToSend);

            using var message = _message.CreateMessage(From, joined);
            await SendEmailAsync(message);

            message?.Dispose();

        }

    }
}

