using WebDevelopment.Common.Requests.SalaryRange;

namespace WebDevelopment.Domain.SalaryRange.Services;

public interface ISalaryRangeService
{
    Task<IEnumerable<SalaryRangeWithIdRequest>> GetAllAsync();

    Task<SalaryRangeWithIdRequest> GetById(int id);

    Task<IEnumerable<SalaryRangeWithIdRequest>> GetByPositionName(string positionName);

    Task<bool> AddNewSalaryRangeAsync(NewSalaryRangeRequest request);

    Task<bool> UpdateSalaryRangeAsync(SalaryRangeWithIdRequest requestWithId);
}