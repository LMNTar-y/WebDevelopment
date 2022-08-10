using Microsoft.EntityFrameworkCore;
using WebDevelopment.Common.Requests.Position;
using WebDevelopment.Domain.Position;
using WebDevelopment.Infrastructure.Models;

namespace WebDevelopment.Infrastructure.Repositories;

public class PositionRepository : IPositionRepository
{
    private readonly WebDevelopmentContext _context;

    public PositionRepository(WebDevelopmentContext context)
    {
        _context = context ?? throw new ArgumentException($"{nameof(context)} was not downloaded from DI"); 
    }

    public async Task<IEnumerable<PositionWithIdRequest>> GetAll()
    {
        var result = await _context.Positions.ToListAsync();
        return result.Select(Map);
    }

    public async Task<NewPositionRequest> Add(NewPositionRequest position)
    {
        var result = Map(position);
        _context.Positions.Add(result);
        await _context.SaveChangesAsync();
        return position;
    }

    public async Task<PositionWithIdRequest> Update(PositionWithIdRequest position)
    {
        var result = Map(position);
        _context.Positions.Update(result);
        await _context.SaveChangesAsync();
        return position;
    }

    #region Mappers
    private static Position Map(NewPositionRequest positionRequest)
    {
        return new Position
        {
            Name = positionRequest.Name,
            ShortName = positionRequest.ShortName
        };
    }

    private static Position Map(PositionWithIdRequest positionWithId)
    {
        return new Position
        {
            Id = positionWithId.Id,
            Name = positionWithId.Name,
            ShortName = positionWithId.ShortName,
        };
    }

    private static PositionWithIdRequest Map(Position position)
    {
        return new PositionWithIdRequest
        {
            Id = position.Id,
            Name = position.Name,
            ShortName = position.ShortName,
        };
    }

    #endregion
}