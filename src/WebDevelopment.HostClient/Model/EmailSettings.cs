namespace WebDevelopment.HostClient.Model;

public class EmailSettings
{
    public string? EmailSubject { get; set; }

    public string? EmailBody { get; set; }

    public EmailProvider? CurrentProvider { get; set; }
}