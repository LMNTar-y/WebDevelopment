using WebDevelopment.HostClient.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace WebDevelopment.HostClient;

public class TaskExpirationNotificationService : CronJobService
{
    private readonly ITaskExpirationWorker _worker;
    private readonly ISenderClient _emailClient;

    public TaskExpirationNotificationService(IScheduleConfig<TaskExpirationNotificationService> config, ISenderClient emailClient, ITaskExpirationWorker worker)
        : base(config.CronExpression, config.TimeZoneInfo)
    {
        _emailClient = emailClient;
        _worker = worker;
    }

    public override Task DoWork(CancellationToken cancellationToken)
    {
        try
        {
            var emails = _worker.GetReceiversToSend();
            _emailClient.SendNotification(emails);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return Task.CompletedTask;
    }

}