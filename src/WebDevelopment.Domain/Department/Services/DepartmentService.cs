using WebDevelopment.Common.Requests.Department;

namespace WebDevelopment.Domain.Department.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentService(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<IEnumerable<DepartmentWithIdRequest>> GetAllAsync()
    {
        var result = await _departmentRepository.GetAll();
        return result;
    }

    public async Task<DepartmentWithIdRequest> GetById(Guid id)
    {
        var result = (await _departmentRepository.GetAll()).FirstOrDefault(x => x.Id == id) ??
                     throw new ArgumentNullException(nameof(id), $"Department with id:\"{id}\" has not fount in the DataBase");

        return result;
    }

    public async Task<DepartmentWithIdRequest> GetByName(string name)
    {
        var result = (await _departmentRepository.GetAll()).FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)) ??
                     throw new ArgumentNullException(nameof(name), $"Department with name: \"{name}\" has not fount in the DataBase");

        return result;
    }

    public async Task<bool> AddNewDepartmentAsync(NewDepartmentRequest newDepartmentRequest)
    {
        await _departmentRepository.Add(newDepartmentRequest);
        return true;
    }

    public async Task<bool> UpdateDepartmentAsync(DepartmentWithIdRequest departmentWithIdRequest)
    {
        await _departmentRepository.Update(departmentWithIdRequest);
        return true;
    }
}