using Microsoft.Extensions.Logging;
using WebDevelopment.HostClient.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace WebDevelopment.HostClient;

public class TaskExpirationNotificationService : CronJobService
{
    private readonly ITaskExpirationWorker _worker;
    private readonly ISenderClient _emailClient;
    private readonly ILogger<TaskExpirationNotificationService> _logger;

    public TaskExpirationNotificationService(IScheduleConfig<TaskExpirationNotificationService> config, ISenderClient emailClient, ITaskExpirationWorker worker, ILogger<TaskExpirationNotificationService> logger)
        : base(config.CronExpression, config.TimeZoneInfo)
    {
        _emailClient = emailClient ?? throw new ArgumentNullException(nameof(emailClient), "Impossible to get ISenderClient through DI");
        _worker = worker ?? throw new ArgumentNullException(nameof(worker), "Impossible to get ITaskExpirationWorker through DI");
        _logger = logger;
    }

    public override Task DoWork(CancellationToken cancellationToken)
    {
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

        return Task.CompletedTask;
    }

}