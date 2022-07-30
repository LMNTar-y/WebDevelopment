using System.Text;
using Microsoft.Extensions.Configuration;
using Moq;
using WebDevelopment.Email.Model;
using WebDevelopment.Email.Providers;

namespace WebDevelopment.Email.Tests
{
    public class ServiceProviderMock : Mock<IServiceProvider>
    {
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
            
            return this;
        }
    }
}
