
using WebDevelopment.Common.Requests.Department;

namespace WebDevelopment.Domain.Department
{
    public interface IDepartmentRepository : IDefaultRepository<DepartmentWithIdRequest, NewDepartmentRequest>
    {

    }
}
