using WebDevelopment.Common.Requests.UserTask;

namespace WebDevelopment.Domain.UserTask.Services;

public class UserTaskService : IUserTaskService
{
    private readonly IUserTaskRepository _repository;

    public UserTaskService(IUserTaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<UserTaskWithIdRequest>> GetAllAsync()
    {
        var result = await _repository.GetAll();
        return result;
    }

    public async Task<UserTaskWithIdRequest> GetById(int id)
    {
        var result = (await _repository.GetAll()).First(x => x.Id == id);
        return result;
    }

    public async Task<bool> AddNewUseTaskAsync(NewUserTaskRequest request)
    {
        await _repository.Add(request);
        return true;
    }

    public async Task<bool> UpdateUserTaskAsync(UserTaskWithIdRequest requestWithId)
    {
        await _repository.Update(requestWithId);
        return true;
    }
}