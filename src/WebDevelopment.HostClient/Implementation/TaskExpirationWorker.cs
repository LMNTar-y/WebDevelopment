using Microsoft.Extensions.DependencyInjection;
using WebDevelopment.HostClient.Interfaces;
using WebDevelopment.Infrastructure;

namespace WebDevelopment.HostClient.Implementation;

public class TaskExpirationWorker : ITaskExpirationWorker
{
    private readonly IServiceScopeFactory _scopeFactory;

    public TaskExpirationWorker(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public List<string> GetReceiversToSend()
    {
        var emailsToSend = new List<string>();
        using (var scope = _scopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<WebDevelopmentContext>();

            var samples = context.Users.Join(context.UserTasks, u => u.Id, t => t.UserId, (u, t) => new { u, t })
                .Where(@t1 => @t1.t.FinishDate == null)
                .Select(@t1 => new { @t1.u.UserEmail, @t1.t.ValidTill })
                .ToList();

            if (samples.Count > 0)
                emailsToSend.AddRange(from sample in samples
                                      where sample.UserEmail != null && sample.ValidTill - DateTimeOffset.Now < TimeSpan.FromDays(1)
                                      select sample.UserEmail);
        }

        return emailsToSend;
    }
}