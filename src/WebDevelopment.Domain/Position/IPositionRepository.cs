
using WebDevelopment.Common.Requests.Position;

namespace WebDevelopment.Domain.Position
{
    public interface IPositionRepository : IDefaultRepository<PositionWithIdRequest, NewPositionRequest>
    {
    }
}
