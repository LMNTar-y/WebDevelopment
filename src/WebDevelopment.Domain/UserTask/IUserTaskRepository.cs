using WebDevelopment.Common.Requests.UserTask;

namespace WebDevelopment.Domain.UserTask;

public interface IUserTaskRepository : IDefaultRepository<UserTaskWithIdRequest, NewUserTaskRequest>
{
    
}