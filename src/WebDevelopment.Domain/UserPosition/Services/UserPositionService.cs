using WebDevelopment.Common.Requests.UserPosition;

namespace WebDevelopment.Domain.UserPosition.Services;

public class UserPositionService : IUserPositionService
{
    private readonly IUserPositionRepository _userPositionRepository;

    public UserPositionService(IUserPositionRepository userPositionRepository)
    {
        _userPositionRepository = userPositionRepository;
    }

    public async Task<IEnumerable<UserPositionWithIdRequest>> GetAllAsync()
    {
        var result = await _userPositionRepository.GetAll();
        return result;
    }

    public async Task<UserPositionWithIdRequest> GetById(int id)
    {
        var result = (await _userPositionRepository.GetAll()).FirstOrDefault(x => x.Id == id) ??
                     throw new ArgumentNullException(nameof(id), $"UserPosition with id: \"{id}\" was not found");
        return result;
    }

    public async Task<IEnumerable<UserPositionWithIdRequest>> GetByUserNameAsync(string firstName, string lastName)
    {
        var result = (await _userPositionRepository.GetAll()).Where(x =>
                         !string.IsNullOrWhiteSpace(x?.User?.FirstName) &&
                         !string.IsNullOrWhiteSpace(x?.User?.SecondName) && x.User.FirstName.Contains(firstName) &&
                         x.User.SecondName.Contains(lastName)) ??
                     throw new ArgumentNullException(nameof(firstName),
                         $"UserPosition with firstName: \"{firstName}\" and lastName: \"{lastName}\" was not found");
        return result;
    }

    public async Task<bool> AddNewUserPositionAsync(NewUserPositionRequest request)
    {
        await _userPositionRepository.Add(request);
        return true;
    }

    public async Task<bool> UpdateUserPositionAsync(UserPositionWithIdRequest requestWithId)
    {
        await _userPositionRepository.Update(requestWithId);
        return true;
    }
}