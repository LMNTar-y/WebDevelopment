namespace WebDevelopment.Email.Providers.Interfaces;

public interface IEmailProvider
{
    public Task SendNotification(List<string> emailsToSend);
}