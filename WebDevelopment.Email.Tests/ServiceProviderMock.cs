using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using WebDevelopment.Email.Providers;
using WebDevelopment.Email.Settings;

namespace WebDevelopment.Email.Tests
{
    public class ServiceProviderMock : Mock<IServiceProvider>
    {
        private readonly Mock<ILogger<EmailSmtpClientHelper>> _smtpClientHelperLoggerMock = new();
        private readonly Mock<ILogger<BaseEmailProvider>> _emailProviderLoggerMock = new();

        public ServiceProviderMock Setup_ValidConfiguration()
        {
            var appSettings = @"{""EmailSettings"":{
            ""EmailSubject"": ""test"",
            ""EmailBody"": ""test body"",
            ""EmailProviderSettings"" :{
                ""Yandex"": {
                    ""SmtpHost"": ""smtp."",
                    ""SmtpPort"": ""1"",
                    ""EmailLogin"": ""test"",
                    ""EmailSendFrom"": ""test@test.test"",
                    ""EmailPassword"": ""W0PUPM6NBUWfsZkcPpy2Rb05bDxtNYW7iTpGui6tkI4=""
            }}}}";

            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(appSettings)));

            var configuration = configurationBuilder.Build();
            this.Setup(x => x.GetService(typeof(IConfiguration))).Returns(configuration);
            this.Setup(x => x.GetService(typeof(ILogger<EmailSmtpClientHelper>))).Returns(_smtpClientHelperLoggerMock.Object);
            this.Setup(x => x.GetService(typeof(ILogger<BaseEmailProvider>))).Returns(_emailProviderLoggerMock.Object);

            return this;
        }
    }
}
