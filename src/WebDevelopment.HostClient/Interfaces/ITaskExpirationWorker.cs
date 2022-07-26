namespace WebDevelopment.HostClient.Interfaces;

public interface ITaskExpirationWorker
{
    List<string> GetReceiversToSend();
}