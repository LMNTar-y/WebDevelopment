using Moq;
using WebDevelopment.Email.Model;
using WebDevelopment.Email.Settings;
using WebDevelopment.HostClient.Tests.ServiceMocks;

namespace WebDevelopment.HostClient.Tests.ServiceTests
{
    public class TaskExpirationNotificationJobTests
    {
        private readonly EmailProviderMock _emailProviderMock = new();
        private readonly JobExecutionContextMock _contextMock = new();
        private readonly ServiceProviderMock _serviceProviderMock = new();
        private readonly Mock<EmailProviderSetupFactory> _emailProviderSetupFactoryMock = new(new Mock<IServiceProvider>().Object);
        private readonly TaskExpirationNotificationJob _sut;

        public TaskExpirationNotificationJobTests()
        {
            _serviceProviderMock.Setup_ValidConfiguration();
            _emailProviderSetupFactoryMock.Setup(x => x.Create(It.IsAny<EmailProviderName>()))
                .Returns(_emailProviderMock.Object);
            _serviceProviderMock.Setup(x => x.GetService(typeof(EmailProviderSetupFactory)))
                .Returns(_emailProviderSetupFactoryMock.Object);

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
            _serviceProviderMock.TaskExpirationWorkerMock = new TaskExpirationWorkerMock().Setup_GetReceiversToSendMethod_ReturnsListWithRecord();
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
            _serviceProviderMock.TaskExpirationWorkerMock = new TaskExpirationWorkerMock().Setup_GetReceiversToSendMethod_ThrowException();
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