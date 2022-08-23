using WebDevelopment.Common.Requests.Position;
using WebDevelopment.Domain.IRepos;
using WebDevelopment.Infrastructure.Models;

namespace WebDevelopment.Infrastructure.Repos;

public class PositionRepo : GenericRepository<Position>, IPositionRepo
{
    public PositionRepo(WebDevelopmentContext context) : base(context)
    {
    }

    public new async Task<IEnumerable<IPositionRequest>> GetAllAsync(string includeProperties = "")
    {
        var result = await base.GetAllAsync(includeProperties);
        return result.Select(Map);
    }

    public new async Task<IPositionRequest> GetByIdAsync(object id)
    {
        var result = await base.GetByIdAsync(id);
        return Map(result);
    }

    public async Task<IPositionRequest> GetByNameAsync(string name)
    {
        var result = (await base.GetAllAsync()).FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)) ??
                     throw new ArgumentNullException(nameof(name), $"Position with name: \"{name}\" has not fount in the DataBase");

        return Map(result);
    }

    public async Task<IPositionRequest> AddAsync(IPositionRequest entity)
    {
        var result = Map((NewPositionRequest) entity);
        await AddAsync(result);
        return entity;
    }

    public async Task<IPositionRequest> UpdateAsync(IPositionRequest entity)
    {
        var result = Map((PositionWithIdRequest)entity);
        await UpdateAsync(result);
        return entity;
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