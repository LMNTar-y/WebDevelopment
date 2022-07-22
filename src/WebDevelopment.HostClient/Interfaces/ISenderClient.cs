namespace WebDevelopment.HostClient.Interfaces;

public interface ISenderClient
{
    void SendNotification(List<string> emailsToSend);
}