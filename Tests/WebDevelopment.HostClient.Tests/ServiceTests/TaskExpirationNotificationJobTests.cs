using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using WebDevelopment.Email.Model;
using WebDevelopment.Email.Providers.Interfaces;
using WebDevelopment.Email.Settings;
using WebDevelopment.HostClient.Interfaces;
using WebDevelopment.HostClient.Tests.ServiceMocks;

namespace WebDevelopment.HostClient.Tests.ServiceTests
{
    public class TaskExpirationNotificationJobTests
    {
        private readonly TaskExpirationWorkerMock _taskExpirationWorkerMock;
        private readonly EmailProviderMock _emailProviderMock;
        private readonly JobExecutionContextMock _contextMock;
        private readonly Mock<ILogger<TaskExpirationNotificationJob>> _loggerMock = new();
        private readonly Mock<IServiceProvider> _serviceProviderMock = new();
        private readonly Mock<IConfiguration> _configurationMock = new();
        private readonly Mock<EmailProviderSetupFactory> _emailProviderSetupFactoryMock = new(new Mock<IServiceProvider>().Object);
        private readonly TaskExpirationNotificationJob _sut;

        public TaskExpirationNotificationJobTests()
        {
            _taskExpirationWorkerMock = new TaskExpirationWorkerMock();
            _emailProviderMock = new EmailProviderMock();
            _contextMock = new JobExecutionContextMock();
            _serviceProviderMock.Setup(x => x.GetService(typeof(ITaskExpirationWorker)))
                .Returns(_taskExpirationWorkerMock.Object);
            _serviceProviderMock.Setup(x => x.GetService(typeof(ILogger<TaskExpirationNotificationJob>)))
                .Returns(_loggerMock.Object);
            _serviceProviderMock.Setup(x => x.GetService(typeof(IConfiguration)))
                .Returns(_configurationMock.Object);
            _emailProviderSetupFactoryMock.Setup(x => x.Create(It.IsAny<EmailProviderName>()))
                .Returns(_emailProviderMock.Object);
            _serviceProviderMock.Setup(x => x.GetService(typeof(EmailProviderSetupFactory)))
                .Returns(_emailProviderSetupFactoryMock.Object);
            _configurationMock.SetupGet(x => x[$"{nameof(EmailSettings)}:CurrentProvider"])
                .Returns("Yandex");
            _sut = new TaskExpirationNotificationJob(_serviceProviderMock.Object);
        }

        [Fact]
        public void Test_Constructor_When_DependenciesInitFailure_Result_Exception()
        {
            //Arrange
            //Act
            var act = new Action(() =>
            {
                new TaskExpirationNotificationJob(null);
            });
            
            var exception = Record.Exception(act);

            //Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public void Test_Execute_NoExceptionThrown()
        {
            //Arrange
            _taskExpirationWorkerMock.Setup_GetReceiversToSendMethod_ReturnsListWithRecord();
            _emailProviderMock.Setup_SendNotificationMethod_ReturnsVoid();
            _contextMock.Setup();
            
            var act = new Action(() =>
            {
                _sut.Execute(_contextMock.Object);
            });
            var exception = Record.Exception(act);

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void Test_Execute_WhenExceptionThrown_ResultExceptionCaught()
        {
            //Arrange
            _taskExpirationWorkerMock.Setup_GetReceiversToSendMethod_ThrowException();
            _emailProviderMock.Setup_SendNotificationMethod_ThrowException();
            _contextMock.Setup();

            var act = new Action(() =>
            {
                _sut.Execute(_contextMock.Object);
            });
            var exception = Record.Exception(act);

            //Assert
            Assert.Null(exception);
        }

    }
}