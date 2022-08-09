
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using WebDevelopment.Email.Model;
using WebDevelopment.Email.Settings;
using WebDevelopment.HostClient.Interfaces;

namespace WebDevelopment.HostClient
{
    public class TaskExpirationNotificationJob : IJob
    {

        private readonly ILogger<TaskExpirationNotificationJob> _logger;
        private readonly ITaskExpirationWorker _worker;
        private readonly EmailProviderSetupFactory _emailProviderFactory;
        private readonly IConfiguration _configuration;

        public TaskExpirationNotificationJob(IServiceProvider serviceProvider)
        {
            _worker = serviceProvider.GetRequiredService<ITaskExpirationWorker>();
            _logger = serviceProvider.GetRequiredService<ILogger<TaskExpirationNotificationJob>>();
            _emailProviderFactory = serviceProvider.GetRequiredService<EmailProviderSetupFactory>();
            _configuration = serviceProvider.GetRequiredService<IConfiguration>();
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("TaskExpirationNotificationJob - started");

            try
            {
                var emailProviderName =
                    Enum.Parse<EmailProviderName>(_configuration[$"{nameof(EmailSettings)}:CurrentProvider"]);

                var emailProvider = _emailProviderFactory.Create(emailProviderName);
                var emails = _worker.GetReceiversToSend();

                if (emails?.Count > 0)
                {
                    emailProvider.SendNotification(emails);
                }
                else
                {
                    _logger.LogInformation("No emails in db with near expiration time");
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "TaskExpirationNotificationService - DoWork - {0}",
                    ex.ToString());
            }

            _logger.LogInformation("TaskExpirationNotificationJob - finished");

            return Task.CompletedTask;
        }
    }
}
