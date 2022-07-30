using Moq;
using WebDevelopment.Email.Providers.Interfaces;

namespace WebDevelopment.HostClient.Tests.ServiceMocks
{
    public class EmailProviderMock : Mock<IEmailProvider>
    {
        public EmailProviderMock Setup_SendNotificationMethod_ReturnsVoid()
        {
            this.Setup(x => x.SendNotification(It.IsAny<List<string>>()));
            return this;
        }

        public EmailProviderMock Setup_SendNotificationMethod_ThrowException()
        {
            this.Setup(x => x.SendNotification(It.IsAny<List<string>>())).Callback(() => throw new Exception());
            return this;
        }
    }
}
