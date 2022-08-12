using Microsoft.EntityFrameworkCore;
using WebDevelopment.Common.Requests.Task;
using WebDevelopment.Domain.Task;
using WebDevelopment.Infrastructure.Models;

namespace WebDevelopment.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly WebDevelopmentContext _context;

    public TaskRepository(WebDevelopmentContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskWithIdRequest>> GetAll()
    {
        var result = await _context.Tasks.ToListAsync();
        return result.Select(Map);
    }

    public async Task<NewTaskRequest> Add(NewTaskRequest item)
    {
        var result = Map(item);
        _context.Tasks.Add(result);
        await _context.SaveChangesAsync();
        return item;

    }

    public async Task<TaskWithIdRequest> Update(TaskWithIdRequest itemWithId)
    {
        var result = Map(itemWithId);
        _context.Tasks.Update(result);
        await _context.SaveChangesAsync();
        return itemWithId;
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