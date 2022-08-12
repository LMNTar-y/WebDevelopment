using WebDevelopment.Common.Requests.SalaryRange;

namespace WebDevelopment.Domain.SalaryRange.Services;

public class SalaryRangeService : ISalaryRangeService
{
    private readonly ISalaryRangeRepository _repository;

    public SalaryRangeService(ISalaryRangeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<SalaryRangeWithIdRequest>> GetAllAsync()
    {
        var result = await _repository.GetAll();
        return result;
    }

    public async Task<SalaryRangeWithIdRequest> GetById(int id)
    {
        var result = (await _repository.GetAll()).FirstOrDefault(s => s.Id == id) ??
                     throw new ArgumentNullException(nameof(id), $"SalaryRange with id: \"{id}\" does not exist");
        return result;
    }
    public async Task<IEnumerable<SalaryRangeWithIdRequest>> GetByPositionName(string positionName)
    {
        var result = (await _repository.GetAll())
                     .Where(s => !string.IsNullOrWhiteSpace(s.PositionName) && s.PositionName.Contains(positionName, StringComparison.CurrentCultureIgnoreCase)) ??
                     throw new ArgumentNullException(nameof(positionName), $"SalaryRange with positionName: \"{positionName}\" does not exist");
        return result;
    }

    public async Task<bool> AddNewSalaryRangeAsync(NewSalaryRangeRequest request)
    {
        await _repository.Add(request);
        return true;
    }

    public async Task<bool> UpdateSalaryRangeAsync(SalaryRangeWithIdRequest requestWithId)
    {
        await _repository.Update(requestWithId);
        return true;
    }
}