namespace WebDevelopment.HostClient.Model;

public class EmailProviderSettings
{
    public string? SmtpHost { get; set; }

    public int SmtpPort { get; set; }

    public string? EmailLogin { get; set; }

    public string? EmailSendFrom { get; set; }

    public string? EmailPassword { get; set; }
}