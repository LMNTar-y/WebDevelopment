namespace WebDevelopment.Email.Model;

public class EmailSettings
{
    public string? EmailSubject { get; set; }

    public string? EmailBody { get; set; }

    public EmailProviderName? CurrentProvider { get; set; }
}