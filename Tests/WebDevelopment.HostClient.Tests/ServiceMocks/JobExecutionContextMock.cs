using Moq;
using Quartz;

namespace WebDevelopment.HostClient.Tests.ServiceMocks
{
    public class JobExecutionContextMock : Mock<IJobExecutionContext>  
    {
        public JobExecutionContextMock Setup()
        {
            return this;
        }
    }
}
