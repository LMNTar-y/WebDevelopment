using Microsoft.Extensions.Logging;
using Moq;
using WebDevelopment.HostClient.Tests.ServiceMocks;

namespace WebDevelopment.HostClient.Tests.ServiceTests
{
    public class TaskExpirationNotificationServiceTests
    {
        private readonly ITaskExpirationWorkerMock _taskExpirationWorkerMock;
        private readonly ISenderClientMock _senderClientMock;
        private readonly Mock<ILogger<TaskExpirationNotificationService>> _loggerMock = new();
        private readonly Mock<ScheduleConfig<TaskExpirationNotificationService>> _scheduleConfig = new();
        private readonly TaskExpirationNotificationService _sut;

        public TaskExpirationNotificationServiceTests()
        {
            _taskExpirationWorkerMock = new ITaskExpirationWorkerMock();
            _senderClientMock = new ISenderClientMock();
            var f = _scheduleConfig.Object.CronExpression = "* * * * *";
            _sut = new(_scheduleConfig.Object, _senderClientMock.Object,
                _taskExpirationWorkerMock.Object, _loggerMock.Object);
        }

        [Fact]
        public void Test_Constructor_When_DependenciesInitFailure_Result_Exception()
        {
            //Arrange
            //Act
            var act = new Action(() =>
            {
                new TaskExpirationNotificationService(null, null, null, null);
            });

            var exception = Record.Exception(act);

            //Assert
            Assert.NotNull(exception);
        }

        [Fact]
        public void Test_DoWork_NoExceptionThrown()
        {
            //Arrange
            _taskExpirationWorkerMock.Setup_GetReceiversToSendMethod_ReturnsListWithRecord();
            _senderClientMock.Setup_SendNotificationMethod_WithExistingList();
            //Act
            
            var act = new Action(() =>
            {
                _sut.DoWork(new CancellationToken());
            });
            var exception = Record.Exception(act);

            //Assert
            Assert.Null(exception);
        }


    }
}