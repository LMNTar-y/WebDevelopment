
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using WebDevelopment.Email.Model;
using WebDevelopment.HostClient.Interfaces;

namespace WebDevelopment.HostClient.Tests.ServiceMocks
{
    public class ServiceProviderMock : Mock<IServiceProvider>
    {
        private readonly Mock<ILogger<TaskExpirationNotificationJob>> _loggerMock = new();
        private readonly Mock<IConfiguration> _configurationMock = new();
        private TaskExpirationWorkerMock _taskExpirationWorkerMock = new();
        public TaskExpirationWorkerMock TaskExpirationWorkerMock
        {
            set => _taskExpirationWorkerMock = value;
        }


        public ServiceProviderMock Setup_ValidConfiguration()
        {
            this.Setup(x => x.GetService(typeof(ILogger<TaskExpirationNotificationJob>)))
                .Returns(_loggerMock.Object);
            this.Setup(x => x.GetService(typeof(IConfiguration)))
                .Returns(_configurationMock.Object);
            _configurationMock.SetupGet(x => x[$"{nameof(EmailSettings)}:CurrentProvider"])
                .Returns("Yandex");
            this.Setup(x => x.GetService(typeof(ITaskExpirationWorker)))
                .Returns(_taskExpirationWorkerMock.Object);

            return this;
        }
    }
}
