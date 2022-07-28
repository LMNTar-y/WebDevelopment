namespace WebDevelopment.Email.Model;

public class EmailProviderSettings
{
    public string? SmtpHost { get; set; }

    public int SmtpPort { get; set; }

    public string? EmailLogin { get; set; }

    public string EmailSendFrom { get; set; } = null!;

    public string EmailPassword { get; set; } = null!;
}