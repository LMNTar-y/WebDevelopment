using WebDevelopment.Common.Requests.Task;

namespace WebDevelopment.Domain.Task.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository ?? throw new ArgumentException($"{nameof(taskRepository)} was not downloaded from DI");
    }

    public async Task<IEnumerable<TaskWithIdRequest>> GetAllAsync()
    {
        var result = await _taskRepository.GetAll();
        return result;
    }

    public async Task<TaskWithIdRequest> GetById(int id)
    {
        var result = (await _taskRepository.GetAll()).FirstOrDefault(t => t.Id == id) ??
                     throw new ArgumentNullException(nameof(id), $"Task with id:\"{id}\" has not fount in the DataBase");
        return result;
    }

    public async Task<TaskWithIdRequest> GetByName(string name)
    {
        var result = (await _taskRepository.GetAll()).FirstOrDefault(t => string.Equals(t.Name, name, StringComparison.CurrentCultureIgnoreCase)) ??
                     throw new ArgumentNullException(nameof(name), $"Task with name:\"{name}\" has not fount in the DataBase");
        return result;
    }

    public async Task<bool> AddNewTaskAsync(NewTaskRequest request)
    {
        await _taskRepository.Add(request);
        return true;
    }

    public async Task<bool> UpdateTaskAsync(TaskWithIdRequest requestWithId)
    {
        await _taskRepository.Update(requestWithId);
        return true;
    }
}