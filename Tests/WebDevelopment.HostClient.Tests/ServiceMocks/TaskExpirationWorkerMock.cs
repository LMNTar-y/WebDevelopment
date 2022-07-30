using Moq;
using WebDevelopment.HostClient.Interfaces;

namespace WebDevelopment.HostClient.Tests.ServiceMocks
{
    public class TaskExpirationWorkerMock : Mock<ITaskExpirationWorker>
    {
        public TaskExpirationWorkerMock Setup_GetReceiversToSendMethod_ReturnsEmptyList()
        {
            this.Setup(x => x.GetReceiversToSend()).Returns(new List<string>());
            return this;
        }

        public TaskExpirationWorkerMock Setup_GetReceiversToSendMethod_ReturnsListWithRecord()
        {
            this.Setup(x => x.GetReceiversToSend()).Returns(new List<string>(){"somemail@mail.mail"});
            return this;
        }

        public TaskExpirationWorkerMock Setup_GetReceiversToSendMethod_ReturnsNull()
        {
            this.Setup(x => x.GetReceiversToSend()).Returns(() => null);
            return this;
        }

        public TaskExpirationWorkerMock Setup_GetReceiversToSendMethod_ThrowException()
        {
            this.Setup(x => x.GetReceiversToSend()).Callback(() => throw new Exception());
            return this;
        }
    }
}
