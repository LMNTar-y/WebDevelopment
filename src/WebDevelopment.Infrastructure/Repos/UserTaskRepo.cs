using Microsoft.EntityFrameworkCore;
using WebDevelopment.Common.Requests.Task;
using WebDevelopment.Common.Requests.User;
using WebDevelopment.Common.Requests.UserTask;
using WebDevelopment.Domain.IRepos;
using WebDevelopment.Infrastructure.Models;
using Task = WebDevelopment.Infrastructure.Models.Task;

namespace WebDevelopment.Infrastructure.Repos;

public class UserTaskRepo : GenericRepository<UserTask>, IUserTaskRepo
{
    private readonly GenericRepository<User> _user;
    private readonly GenericRepository<Task> _task;
    public UserTaskRepo(WebDevelopmentContext context) : base(context)
    {
        _user = new GenericRepository<User>(context);
        _task = new GenericRepository<Task>(context);
    }

    public new async Task<IEnumerable<IUserTaskRequest>> GetAllAsync(string includeProperties = "")
    {
        includeProperties += "User, Task";
        var result = await base.GetAllAsync(includeProperties);

        return result.Select(Map);
    }

    public new async Task<IUserTaskRequest> GetByIdAsync(object id)
    {
        var includeProperties = "User, Task";
        var result = (await base.GetAllAsync(includeProperties)).Single(x => x.Id == (int)id);
        return Map(result);
    }

    public async Task<IUserTaskRequest> AddAsync(IUserTaskRequest entity)
    {
        var result = Map((NewUserTaskRequest)entity);
        var user = (await _user.GetAllAsync()).First(u =>
            string.Equals(entity?.User?.FirstName?.ToLower(), u?.FirstName?.ToLower()) &&
            string.Equals(entity.User.SecondName?.ToLower(), u.SecondName?.ToLower()) &&
            string.Equals(entity.User.UserEmail?.ToLower(), u.UserEmail?.ToLower()));
        var task = (await _task.GetAllAsync()).First(t => string.Equals(t.Name, entity.Task.Name));
        result.User = user;
        result.Task = task;
        await AddAsync(result);
        return entity;
    }

    public async Task<IUserTaskRequest> UpdateAsync(IUserTaskRequest entity)
    {
        var result = Map((UserTaskWithIdRequest)entity);
        var user = (await _user.GetAllAsync()).First(u =>
            string.Equals(entity?.User?.FirstName?.ToLower(), u?.FirstName?.ToLower()) &&
            string.Equals(entity.User.SecondName?.ToLower(), u.SecondName?.ToLower()) &&
            string.Equals(entity.User.UserEmail?.ToLower(), u.UserEmail?.ToLower()));
        var task = (await _task.GetAllAsync()).First(t => string.Equals(t.Name, entity.Task.Name));
        result.User = user;
        result.Task = task;
        await UpdateAsync(result);
        return entity;
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