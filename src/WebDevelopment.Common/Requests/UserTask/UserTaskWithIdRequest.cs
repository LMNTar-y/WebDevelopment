using WebDevelopment.Common.Requests.Task;
using WebDevelopment.Common.Requests.User;

namespace WebDevelopment.Common.Requests.UserTask;

public class UserTaskWithIdRequest : IUserTaskRequest
{
    public int Id { get; set; }
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? FinishDate { get; set; }
    public DateTimeOffset? ValidTill { get; set; }
    public NewUserRequest User { get; set; } = null!;
    public NewTaskRequest Task { get; set; } = null!;
}