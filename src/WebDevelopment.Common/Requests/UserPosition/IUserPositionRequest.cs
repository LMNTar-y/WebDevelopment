using WebDevelopment.Common.Requests.Department;
using WebDevelopment.Common.Requests.Position;
using WebDevelopment.Common.Requests.User;

namespace WebDevelopment.Common.Requests.UserPosition;

public interface IUserPositionRequest
{
    public UserWithIdRequest? User { get; set; }
    public PositionWithIdRequest? Position { get; set; }
    public NewDepartmentRequest? Department { get; set; }
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }

}