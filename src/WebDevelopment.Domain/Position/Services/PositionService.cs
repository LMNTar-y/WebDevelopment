using WebDevelopment.Common.Requests.Position;

namespace WebDevelopment.Domain.Position.Services;

public class PositionService : IPositionService
{
    private readonly IPositionRepository _positionRepository;

    public PositionService(IPositionRepository positionRepository)
    {
        _positionRepository = positionRepository ?? throw new ArgumentException($"{nameof(positionRepository)} was not downloaded from DI");
    }

    public async Task<IEnumerable<PositionWithIdRequest>> GetAllAsync()
    {
        var positions = await _positionRepository.GetAll();
        return positions;
    }

    public async Task<PositionWithIdRequest> GetById(int id)
    {
        var position = (await _positionRepository.GetAll()).FirstOrDefault(x => x.Id == id) ??
                       throw new ArgumentNullException(nameof(id), $"Position with id:\"{id}\" has not fount in the DataBase"); 
        return position;
    }

    public async Task<PositionWithIdRequest> GetByName(string name)
    {
        var position = (await _positionRepository.GetAll()).FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)) ??
                       throw new ArgumentNullException(nameof(name), $"Position with name:\"{name}\" has not fount in the DataBase"); 
        return position;
    }

    public async Task<bool> AddNewPositionAsync(NewPositionRequest request)
    {
        await _positionRepository.Add(request);
        return true;
    }

    public async Task<bool> UpdatePositionAsync(PositionWithIdRequest requestWithId)
    {
        await _positionRepository.Update(requestWithId);
        return true;
    }
}