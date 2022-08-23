using WebDevelopment.Common.Requests.Department;
using WebDevelopment.Domain.IRepos;
using WebDevelopment.Infrastructure.Models;

namespace WebDevelopment.Infrastructure.Repos;

public class DepartmentRepo : GenericRepository<Department>, IDepartmentRepo
{
    public DepartmentRepo(WebDevelopmentContext context) : base(context)
    {
    }

    public new async Task<IEnumerable<IDepartmentRequest>> GetAllAsync(string includeProperties = "")
    {
        var result = await base.GetAllAsync(includeProperties);

        return result.Select(Map);
    }

    public new async Task<IDepartmentRequest> GetByIdAsync(object id)
    {
        var  result = await base.GetByIdAsync(id);
        return Map(result);
    }

    public async Task<IDepartmentRequest> GetByNameAsync(string name)
    {
        var result = (await base.GetAllAsync()).FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)) ??
                     throw new ArgumentNullException(nameof(name), $"Department with name: \"{name}\" has not fount in the DataBase");

        return Map(result);
    }

    public async Task<IDepartmentRequest> AddAsync(IDepartmentRequest entity)
    {
        var result = Map((NewDepartmentRequest)entity);
        await AddAsync(result);
        return entity;
    }

    public async Task<IDepartmentRequest> UpdateAsync(IDepartmentRequest entity)
    {
        var result = Map((DepartmentWithIdRequest)entity);
        await UpdateAsync(result);
        return entity;
    }

    #region Mappers
    private static Department Map(NewDepartmentRequest departmentRequest)
    {
        return new Department
        {
            Name = departmentRequest.Name
        };
    }

    private static Department Map(DepartmentWithIdRequest departmentWithId)
    {
        return new Department
        {
            Id = departmentWithId.Id,
            Name = departmentWithId.Name,
        };
    }

    private static DepartmentWithIdRequest Map(Department department)
    {
        return new DepartmentWithIdRequest
        {
            Id = department.Id,
            Name = department.Name
        };
    }

    #endregion
}