using System.Net.Mail;
using System.Text.Json.Serialization;

namespace WebDevelopment.HostClient.Model
{
    public class SmtpClientSetups
    {
        [JsonPropertyName("emailSubject")]
        public string? EmailSubject { get; set; }

        [JsonPropertyName("emailBody")]
        public string? EmailBody { get; set; }

        [JsonPropertyName("smtpHost")]
        public string? SmtpHost { get; set; }

        [JsonPropertyName("smtpPort")]
        public int SmtpPort { get; set; }

        [JsonPropertyName("emailLogin")]
        public string? EmailLogin { get; set; }

        [JsonPropertyName("emailSendFrom")]
        public string? EmailSendFrom { get; set; }

        [JsonPropertyName("emailPassword")]
        public string? EmailPassword { get; set; }

        [JsonPropertyName("encryptionKey")]
        public string? EncryptionKey { get; set; }
    }
}
