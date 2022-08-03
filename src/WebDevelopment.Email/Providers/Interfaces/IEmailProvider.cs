namespace WebDevelopment.Email.Providers.Interfaces;

public interface IEmailProvider
{
    public Task<bool> SendNotification(List<string> emailsToSend);
}