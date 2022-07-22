using Moq;
using WebDevelopment.HostClient.Interfaces;

namespace WebDevelopment.HostClient.Tests.ServiceMocks
{
    public class ITaskExpirationWorkerMock : Mock<ITaskExpirationWorker>
    {
        public ITaskExpirationWorkerMock Setup_GetReceiversToSendMethod_ReturnsEmptyList()
        {
            this.Setup(x => x.GetReceiversToSend()).Returns(new List<string>());
            return this;
        }

        public ITaskExpirationWorkerMock Setup_GetReceiversToSendMethod_ReturnsListWithRecord()
        {
            this.Setup(x => x.GetReceiversToSend()).Returns(new List<string>(){"somemail@mail.mail"});
            return this;
        }

        public ITaskExpirationWorkerMock Setup_GetReceiversToSendMethod_ReturnsNull()
        {
            this.Setup(x => x.GetReceiversToSend()).Returns(() => null);
            return this;
        }
    }
}
