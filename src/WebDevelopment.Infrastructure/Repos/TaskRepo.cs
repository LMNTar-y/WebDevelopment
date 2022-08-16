using WebDevelopment.Common.Requests.Task;
using WebDevelopment.Domain.IRepos;
using Task = WebDevelopment.Infrastructure.Models.Task;

namespace WebDevelopment.Infrastructure.Repos;

public class TaskRepo : GenericRepository<Task>, ITaskRepo
{
    public TaskRepo(WebDevelopmentContext context) : base(context)
    {
    }

    public new async Task<IEnumerable<ITaskRequest>> GetAllAsync(string includeProperties = "")
    {
        var result = await base.GetAllAsync(includeProperties);
        return result.Select(Map);
    }

    public new async Task<ITaskRequest> GetByIdAsync(object id)
    {
        var task = await base.GetByIdAsync(id);
        return Map(task);
    }

    public async Task<ITaskRequest> GetByName(string name)
    {
        var result = (await base.GetAllAsync()).FirstOrDefault(t => string.Equals(t.Name, name, StringComparison.CurrentCultureIgnoreCase)) ??
                     throw new ArgumentNullException(nameof(name), $"Task with name:\"{name}\" has not fount in the DataBase");

        return Map(result);
    }

    public async Task<ITaskRequest> AddAsync(ITaskRequest entity)
    {
        var result = Map((NewTaskRequest)entity);
        await base.AddAsync(result);
        return entity;
    }

    public async Task<ITaskRequest> UpdateAsync(ITaskRequest entity)
    {
        var result = Map((TaskWithIdRequest)entity);
        await base.UpdateAsync(result);
        return entity;
    }

    #region Mappers
    private static Models.Task Map(NewTaskRequest newTaskRequest)
    {
        return new Models.Task
        {
            Name = newTaskRequest.Name,
            Description = newTaskRequest.Description
        };
    }

    private static Models.Task Map(TaskWithIdRequest taskWithIdRequest)
    {
        return new Models.Task
        {
            Id = taskWithIdRequest.Id,
            Name = taskWithIdRequest.Name,
            Description = taskWithIdRequest.Description,
            CreationDate = DateTimeOffset.Now
        };
    }

    private static TaskWithIdRequest Map(Models.Task task)
    {
        return new TaskWithIdRequest
        {
            Id = task.Id,
            Name = task.Name,
            Description = task.Description
        };
    }

    #endregion
}