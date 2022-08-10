using WebDevelopment.Common.Requests.Position;

namespace WebDevelopment.Domain.Position.Services;

public interface IPositionService
{
    Task<IEnumerable<PositionWithIdRequest>> GetAllAsync();

    Task<PositionWithIdRequest> GetById(int id);

    Task<PositionWithIdRequest> GetByName(string name);

    Task<bool> AddNewPositionAsync(NewPositionRequest request);

    Task<bool> UpdatePositionAsync(PositionWithIdRequest requestWithId);
}