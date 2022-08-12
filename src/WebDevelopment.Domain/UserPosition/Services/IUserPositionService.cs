using WebDevelopment.Common.Requests.UserPosition;

namespace WebDevelopment.Domain.UserPosition.Services;

public interface IUserPositionService
{
    Task<IEnumerable<UserPositionWithIdRequest>> GetAllAsync();

    Task<UserPositionWithIdRequest> GetById(int id);

    Task<IEnumerable<UserPositionWithIdRequest>> GetByUserNameAsync(string firstName, string lastName);

    Task<bool> AddNewUserPositionAsync(NewUserPositionRequest request);

    Task<bool> UpdateUserPositionAsync(UserPositionWithIdRequest requestWithId);
}