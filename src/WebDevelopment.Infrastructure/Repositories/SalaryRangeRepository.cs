using Microsoft.EntityFrameworkCore;
using WebDevelopment.Common.Requests.SalaryRange;
using WebDevelopment.Domain.SalaryRange;
using WebDevelopment.Infrastructure.Models;

namespace WebDevelopment.Infrastructure.Repositories;

public class SalaryRangeRepository : ISalaryRangeRepository
{
    private readonly WebDevelopmentContext _context;

    public SalaryRangeRepository(WebDevelopmentContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SalaryRangeWithIdRequest>> GetAll()
    {
        var result = await _context.SalaryRanges
            .Include(s => s.Country)
            .Include(s => s.Position).ToListAsync();
        return result.Select(Map);
    }

    public async Task<NewSalaryRangeRequest> Add(NewSalaryRangeRequest item)
    {
        var result = Map(item);
        result.Country = GetCountry(item);
        result.Position = GetPosition(item);
        _context.SalaryRanges.Add(result);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<SalaryRangeWithIdRequest> Update(SalaryRangeWithIdRequest itemWithId)
    {
        var result = Map(itemWithId);
        result.Country = GetCountry(itemWithId);
        result.Position = GetPosition(itemWithId);
        _context.SalaryRanges.Update(result);
        await _context.SaveChangesAsync();
        return itemWithId;
    }

    private Country GetCountry(ISalaryRangeRequest salaryRange)
    {
        var result = _context.Countries.FirstOrDefault(c =>
            !string.IsNullOrWhiteSpace(c.Name) && !string.IsNullOrWhiteSpace(salaryRange.CountryName) &&
            string.Equals(c.Name.ToLower(), salaryRange.CountryName.ToLower()));

        return result ?? throw new ArgumentNullException(nameof(salaryRange.CountryName), $"The country {salaryRange.CountryName} was not found in the DB");
    }

    private Position GetPosition (ISalaryRangeRequest salaryRange)
    {
        var result = _context.Positions.FirstOrDefault(p =>
            !string.IsNullOrWhiteSpace(p.Name) && !string.IsNullOrWhiteSpace(salaryRange.PositionName) &&
            string.Equals(p.Name.ToLower(), salaryRange.PositionName.ToLower()));

        return result ?? throw new ArgumentNullException(nameof(salaryRange.PositionName), $"The position {salaryRange.PositionName} was not found in the DB");
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