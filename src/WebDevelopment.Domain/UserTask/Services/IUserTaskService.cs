
using WebDevelopment.Common.Requests.UserTask;

namespace WebDevelopment.Domain.UserTask.Services;

public interface IUserTaskService
{
    Task<IEnumerable<UserTaskWithIdRequest>> GetAllAsync();

    Task<UserTaskWithIdRequest> GetById(int id);

    Task<bool> AddNewUseTaskAsync(NewUserTaskRequest request);

    Task<bool> UpdateUserTaskAsync(UserTaskWithIdRequest requestWithId);
}