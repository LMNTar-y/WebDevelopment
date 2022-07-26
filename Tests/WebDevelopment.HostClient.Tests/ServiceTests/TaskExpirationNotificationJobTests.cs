using Microsoft.Extensions.Logging;
using Moq;
using WebDevelopment.HostClient.Tests.ServiceMocks;

namespace WebDevelopment.HostClient.Tests.ServiceTests
{
    public class TaskExpirationNotificationJobTests
    {
        private readonly ITaskExpirationWorkerMock _taskExpirationWorkerMock;
        private readonly SenderClientMock _senderClientMock;
        private readonly JobExecutionContextMock _contextMock;
        private readonly Mock<ILogger<TaskExpirationNotificationJob>> _loggerMock = new();
        private readonly TaskExpirationNotificationJob _sut;

        public TaskExpirationNotificationJobTests()
        {
            _taskExpirationWorkerMock = new ITaskExpirationWorkerMock();
            _senderClientMock = new SenderClientMock();
            _contextMock = new JobExecutionContextMock();
            _sut = new TaskExpirationNotificationJob(_loggerMock.Object, _senderClientMock.Object,
                _taskExpirationWorkerMock.Object);
        }

        [Fact]
        public void Test_Constructor_When_DependenciesInitFailure_Result_Exception()
        {
            //Arrange
            //Act
            var act = new Action(() =>
            {
                new TaskExpirationNotificationJob(null, null, null);
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
            _senderClientMock.Setup_SendNotificationMethod_WithExistingList();
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