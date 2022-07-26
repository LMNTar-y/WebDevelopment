using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using WebDevelopment.HostClient.Implementation;
using WebDevelopment.HostClient.Model;

namespace WebDevelopment.HostClient.Tests
{
    public class EmailClientTests
    {
        private readonly EmailClient? _sut;
        private readonly Mock<ILogger<EmailClient>> _loggerMock = new Mock<ILogger<EmailClient>>();
        private readonly Mock<IOptions<SmtpClientSetups>> _configurationsMock = new Mock<IOptions<SmtpClientSetups>>();

        public EmailClientTests()
        {
            _sut = new EmailClient(_configurationsMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void Test_Constructor_When_DependenciesInitFailure_Result_Exception()
        {
            //Arrange
            Action act = null;

            //Act
            act = new Action(() =>
            {
                new EmailClient(null, null);
            });

            var exception = Record.Exception(act);

            //Assert
            Assert.NotNull(exception);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void Test_SendNotification_ThrowArgumentException_WhenConfigurationsAreIncorrect(string s)
        {
            //Arrange
            //TODO разбить проверки на 3  теста для каждого параметра
            var smtpClientSetups = new SmtpClientSetups() { EmailSendFrom = s, EmailPassword = s, EncryptionKey = s};
            _configurationsMock.Setup(p => p.Value).Returns(smtpClientSetups);

            //Act
            Action act = new Action(() =>
            {
                _sut.SendNotification(new List<string>(){"somemail@some.some"});
            });

            var exception = Record.Exception(act);

            //Assert
            Assert.NotNull(exception);
        }
    }
}
