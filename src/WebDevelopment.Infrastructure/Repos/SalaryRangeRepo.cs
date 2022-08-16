using WebDevelopment.Common.Requests.SalaryRange;
using WebDevelopment.Domain.IRepos;
using WebDevelopment.Infrastructure.Models;

namespace WebDevelopment.Infrastructure.Repos;

public class SalaryRangeRepo : GenericRepository<SalaryRange>, ISalaryRangeRepo
{
    private readonly GenericRepository<Country> _country;
    private readonly GenericRepository<Position> _position;

    public SalaryRangeRepo(WebDevelopmentContext context) : base(context)
    {
        _country = new GenericRepository<Country>(context);
        _position = new GenericRepository<Position>(context);
    }

    public new async Task<IEnumerable<ISalaryRangeRequest>> GetAllAsync(string includeProperties = "")
    {
        includeProperties += "Country, Position";
        var result = await base.GetAllAsync(includeProperties);
        return result.Select(Map);
    }

    public new async Task<ISalaryRangeRequest> GetByIdAsync(object id)
    {
        var includeProperties = "Country, Position";
        var result = (await base.GetAllAsync(includeProperties)).Single(x => x.Id == (int)id);
        return Map(result);
    }

    public async Task<IEnumerable<ISalaryRangeRequest>> GetByPositionNameAsync(string positionName)
    {
        var result = (await GetAllAsync())
                     .Where(s => !string.IsNullOrWhiteSpace(s.PositionName) && s.PositionName.Contains(positionName, StringComparison.CurrentCultureIgnoreCase)) ??
                     throw new ArgumentNullException(nameof(positionName), $"SalaryRange with positionName: \"{positionName}\" does not exist");
        return result;
    }

    public async Task<ISalaryRangeRequest> AddAsync(ISalaryRangeRequest entity)
    {
        var result = Map((NewSalaryRangeRequest)entity);
        result.Country = await GetCountry(entity);
        result.Position = await GetPosition(entity);
        await AddAsync(result);
        return entity;
    }

    public async Task<ISalaryRangeRequest> UpdateAsync(ISalaryRangeRequest entity)
    {
        var result = Map((SalaryRangeWithIdRequest)entity);
        result.Country = await GetCountry(entity);
        result.Position = await GetPosition(entity);
        await UpdateAsync(result);
        return entity;
    }

    private async Task<Country> GetCountry(ISalaryRangeRequest salaryRange)
    {
        var result = (await _country.GetAllAsync()).Single(x => string.Equals(x.Name, salaryRange.CountryName, StringComparison.CurrentCultureIgnoreCase));

        return result;
    }

    private async Task<Position> GetPosition(ISalaryRangeRequest salaryRange)
    {
        var result = (await _position.GetAllAsync()).Single(x => string.Equals(x.Name, salaryRange.CountryName, StringComparison.CurrentCultureIgnoreCase));

        return result;
    }

    #region Mappers

    private static SalaryRangeWithIdRequest Map(SalaryRange salaryRange)
    {
        return new SalaryRangeWithIdRequest
        {
            Id = salaryRange.Id,
            StartRange = salaryRange.StartRange,
            FinishRange = salaryRange.FinishRange,
            PositionName = salaryRange?.Position?.Name,
            CountryName = salaryRange?.Country?.Name
        };
    }

    private static SalaryRange Map(NewSalaryRangeRequest salaryRange)
    {
        return new SalaryRange()
        {
            StartRange = salaryRange.StartRange,
            FinishRange = salaryRange.FinishRange
        };
    }

    private static SalaryRange Map(SalaryRangeWithIdRequest salaryRange)
    {
        return new SalaryRange()
        {
            Id = salaryRange.Id,
            StartRange = salaryRange.StartRange,
            FinishRange = salaryRange.FinishRange,
            CreationDate = DateTimeOffset.Now
        };
    }

    #endregion
}