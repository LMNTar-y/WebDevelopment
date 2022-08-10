using WebDevelopment.Common.Requests.Department;

namespace WebDevelopment.Domain.Department.Services;

public interface IDepartmentService
{
    Task<IEnumerable<DepartmentWithIdRequest>> GetAllAsync();

    Task<DepartmentWithIdRequest> GetById(Guid id);

    Task<DepartmentWithIdRequest> GetByName(string name);

    Task<bool> AddNewDepartmentAsync(NewDepartmentRequest newDepartmentRequest);

    Task<bool> UpdateDepartmentAsync(DepartmentWithIdRequest departmentWithIdRequest);
}