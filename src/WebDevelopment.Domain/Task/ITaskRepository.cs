using WebDevelopment.Common.Requests.Task;

namespace WebDevelopment.Domain.Task;

public interface ITaskRepository : IDefaultRepository<TaskWithIdRequest, NewTaskRequest>
{
    
}