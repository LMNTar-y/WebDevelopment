using WebDevelopment.Common.Requests.Task;

namespace WebDevelopment.Domain.Task.Services;

public interface ITaskService
{
    Task<IEnumerable<TaskWithIdRequest>> GetAllAsync();

    Task<TaskWithIdRequest> GetById(int id);

    Task<TaskWithIdRequest> GetByName(string name);

    Task<bool> AddNewTaskAsync(NewTaskRequest request);

    Task<bool> UpdateTaskAsync(TaskWithIdRequest requestWithId);
}