using WebDevelopment.Common.Requests.Task;

namespace WebDevelopment.Domain.IRepos;

public interface ITaskRepo : IGenericRepository<ITaskRequest>
{
    Task<ITaskRequest> GetByNameAsync(string name);
}