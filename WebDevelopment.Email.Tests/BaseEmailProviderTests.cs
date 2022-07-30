using System.Net.Mail;
using Microsoft.Extensions.Logging;
using Moq;
using WebDevelopment.Email.Model;
using WebDevelopment.Email.Providers;
using WebDevelopment.Email.Settings;

namespace WebDevelopment.Email.Tests
{
    public class BaseEmailProviderTests
    {
        private readonly ServiceProviderMock _serviceProviderMock = new();
        private readonly Mock<ILogger<EmailSmtpClientHelper>> _smtpClientHelperLoggerMock = new();
        private readonly Mock<ILogger<BaseEmailProvider>> _emailProviderLoggerMock = new();
        private readonly Mock<SmtpClientSendMailAsyncWrapper> _smtpClientWrapperMock = new();
        private readonly BaseEmailProvider _sut;

        public BaseEmailProviderTests()
        {
            _serviceProviderMock.Setup_ValidConfiguration();
            _serviceProviderMock.Setup(x => x.GetService(typeof(ILogger<EmailSmtpClientHelper>))).Returns(_smtpClientHelperLoggerMock.Object);
            _serviceProviderMock.Setup(x => x.GetService(typeof(ILogger<BaseEmailProvider>))).Returns(_emailProviderLoggerMock.Object);
            _sut = new BaseEmailProvider(_serviceProviderMock.Object, EmailProviderName.Yandex);
        }

        [Fact]
        public void Test_Constructor_When_DependenciesInitFailure_Result_Exception()
        {
            //Arrange
            //Act
            var act = new Action(() =>
            {
                new BaseEmailProvider(null, EmailProviderName.Yandex);
            });

            var exception = Record.Exception(act);

            //Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public void Test_SendNotification_NoExceptionThrown()
        {
            //Arrange
            //Act
            var act = new Action(async () =>
            {
                await _sut.SendNotification(new List<string>(){"test@test.test"});
            });
            var exception = Record.Exception(act);

            //Assert
            Assert.Null(exception);
        }

        #region SendEmailAsync
        [Fact]
        public async Task Test_SendEmailAsync_WhenMailMessageIsNull_ReturnFalse()
        {
            //Arrange
            //Act
            var result = await _sut.SendEmailAsync(null);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Test_SendEmailAsync_WhenMailMessageIsEmpty_ReturnFalse()
        {
            //Arrange
            //Act
            var result = await _sut.SendEmailAsync(new MailMessage());

            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Test_SendEmailAsync_WhenMailMessageIsValid_ReturnTrue()
        {
            //Arrange
            var mailMessage = await new MailMassageSetup(_serviceProviderMock.Object).CreateMessageAsync(new MailAddress("xx@xx.xx"), "test@test.test");
            _smtpClientWrapperMock.Setup(x => x.SendAsync(It.IsAny<SmtpClient>(), It.IsAny<MailMessage>()));
            _sut.SmtpClientSendMailAsyncWrapper = _smtpClientWrapperMock.Object;
            //Act
            var result = await _sut.SendEmailAsync(mailMessage);

            //Assert
            Assert.True(result);
        }

        #endregion


    }
}
