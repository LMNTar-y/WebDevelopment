using Microsoft.EntityFrameworkCore;
using WebDevelopment.Common.Requests.Task;
using WebDevelopment.Common.Requests.User;
using WebDevelopment.Common.Requests.UserTask;
using WebDevelopment.Domain.UserTask;
using WebDevelopment.Infrastructure.Models;

namespace WebDevelopment.Infrastructure.Repositories;

public class UserTaskRepository : IUserTaskRepository
{
    private readonly WebDevelopmentContext _context;

    public UserTaskRepository(WebDevelopmentContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserTaskWithIdRequest>> GetAll()
    {
        var result = await _context.UserTasks.Include(u => u.User).Include(t => t.Task).ToListAsync();
        return result.Select(Map);
    }

    public async Task<NewUserTaskRequest> Add(NewUserTaskRequest item)
    {
        var result = Map(item);
        var user = _context.Users.First(u =>
            string.Equals(item.User.FirstName.ToLower(), u.FirstName.ToLower()) &&
            string.Equals(item.User.SecondName.ToLower(), u.SecondName.ToLower()) &&
            string.Equals(item.User.UserEmail.ToLower(), u.UserEmail.ToLower()));
        var task = _context.Tasks.First(t => string.Equals(t.Name, item.Task.Name));
        result.User = user;
        result.Task = task;
        _context.UserTasks.Add(result);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<UserTaskWithIdRequest> Update(UserTaskWithIdRequest itemWithId)
    {
        var result = Map(itemWithId);
        var user = _context.Users.First(u =>
            string.Equals(itemWithId.User.FirstName.ToLower(), u.FirstName.ToLower()) &&
            string.Equals(itemWithId.User.SecondName.ToLower(), u.SecondName.ToLower()) &&
            string.Equals(itemWithId.User.UserEmail.ToLower(), u.UserEmail.ToLower()));
        var task = _context.Tasks.First(t => string.Equals(t.Name, itemWithId.Task.Name));
        result.User = user;
        result.Task = task;
        _context.UserTasks.Update(result);
        await _context.SaveChangesAsync();
        return itemWithId;
    }

    #region Mappers

    private static UserTaskWithIdRequest Map(UserTask userTask)
    {
        if (userTask?.User != null && userTask.Task != null)
            return new UserTaskWithIdRequest()
            {
                Id = userTask.Id,
                StartDate = userTask.StartDate,
                FinishDate = userTask.FinishDate,
                ValidTill = userTask.ValidTill,
                User = new NewUserRequest()
                {
                    FirstName = userTask.User.FirstName,
                    SecondName = userTask.User.SecondName,
                    UserEmail = userTask.User.UserEmail
                },
                Task = new NewTaskRequest()
                {
                    Name = userTask.Task.Name,
                    Description = userTask.Task.Description
                }
            };

        return new UserTaskWithIdRequest();
    }

    private static UserTask Map(NewUserTaskRequest userTask)
    {
        return new UserTask()
        {
            StartDate = DateTimeOffset.Now,
            ValidTill = userTask.ValidTill
        };
    }

    private static UserTask Map(UserTaskWithIdRequest userTask)
    {
        return new UserTask()
        {
            Id = userTask.Id,
            StartDate = DateTimeOffset.Now,
            FinishDate = userTask.FinishDate,
            ValidTill = userTask.ValidTill
        };
    }

    #endregion
}