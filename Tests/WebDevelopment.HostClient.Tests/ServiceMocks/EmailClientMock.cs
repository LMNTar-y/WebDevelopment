using Moq;
using WebDevelopment.HostClient.Interfaces;

namespace WebDevelopment.HostClient.Tests.ServiceMocks
{
    public class ISenderClientMock : Mock<ISenderClient>
    {
        public ISenderClientMock Setup_SendNotificationMethod_WithExistingList()
        {
            this.Setup(x => x.SendNotification(It.Is<List<string>>(l => l.Count > 0)));
            return this;
        }

        public ISenderClientMock Setup_SendNotificationMethod_GetNullInArgument()
        {
            this.Setup(x => x.SendNotification(null));
            return this;
        }
    }
}
