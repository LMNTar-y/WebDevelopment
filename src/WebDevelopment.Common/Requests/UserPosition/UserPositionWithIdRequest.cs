using WebDevelopment.Common.Requests.Department;
using WebDevelopment.Common.Requests.Position;
using WebDevelopment.Common.Requests.User;

namespace WebDevelopment.Common.Requests.UserPosition;

public class UserPositionWithIdRequest : IUserPositionRequest
{
    public int Id { get; set; }
    public UserWithIdRequest? User { get; set; }
    public PositionWithIdRequest? Position { get; set; }
    public NewDepartmentRequest? Department { get; set; }
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }
}