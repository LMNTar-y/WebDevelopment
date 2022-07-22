
using Microsoft.Extensions.Logging;
using Quartz;
using WebDevelopment.HostClient.Interfaces;

namespace WebDevelopment.HostClient
{
    public class TaskExpirationNotificationJob : IJob
    {

        private readonly ILogger<TaskExpirationNotificationJob> _logger;
        private readonly ISenderClient _emailClient;
        private readonly ITaskExpirationWorker _worker;

        public TaskExpirationNotificationJob(ILogger<TaskExpirationNotificationJob> logger, ISenderClient emailClient, ITaskExpirationWorker worker)
        {
            _emailClient = emailClient ?? throw new ArgumentNullException(nameof(emailClient), "Impossible to get ISenderClient through DI");
            _worker = worker ?? throw new ArgumentNullException(nameof(worker), "Impossible to get ITaskExpirationWorker through DI");
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("TaskExpirationNotificationJob - started");
            try
            {
                var emails = _worker.GetReceiversToSend();
                if (emails?.Count > 0)
                {
                    _emailClient.SendNotification(emails);
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
