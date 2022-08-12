using WebDevelopment.Common.Requests.UserPosition;

namespace WebDevelopment.Domain.UserPosition;

public interface IUserPositionRepository : IDefaultRepository<UserPositionWithIdRequest, NewUserPositionRequest>
{
    
}