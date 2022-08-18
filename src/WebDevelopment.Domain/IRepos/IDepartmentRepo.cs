using WebDevelopment.Common.Requests.Department;

namespace WebDevelopment.Domain.IRepos;

public interface IDepartmentRepo : IGenericRepository<IDepartmentRequest>
{
    Task<IDepartmentRequest> GetByNameAsync(string name);
}