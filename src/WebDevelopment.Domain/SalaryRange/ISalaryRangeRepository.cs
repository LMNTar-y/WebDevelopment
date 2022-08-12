using WebDevelopment.Common.Requests.SalaryRange;

namespace WebDevelopment.Domain.SalaryRange;

public interface ISalaryRangeRepository : IDefaultRepository<SalaryRangeWithIdRequest, NewSalaryRangeRequest>
{
    
}