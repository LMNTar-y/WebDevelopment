
using Microsoft.EntityFrameworkCore;
using WebDevelopment.Common.Requests.Department;
using WebDevelopment.Domain.Department;
using WebDevelopment.Infrastructure.Models;

namespace WebDevelopment.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly WebDevelopmentContext _context;

        public DepartmentRepository(WebDevelopmentContext context)
        {
            _context = context ?? throw new ArgumentException($"{nameof(context)} was not downloaded from DI");
        }

        public async Task<IEnumerable<DepartmentWithIdRequest>> GetAll()
        {
            var result = await _context.Departments.ToListAsync();
            return result.Select(Map);
        }

        public async Task<NewDepartmentRequest> Add(NewDepartmentRequest department)
        {
            var result = Map(department);
            _context.Departments.Add(result);
            await _context.SaveChangesAsync();
            return department;
        }

        public async Task<DepartmentWithIdRequest> Update(DepartmentWithIdRequest departmentWithId)
        {
            var result = Map(departmentWithId);
            _context.Departments.Update(result);
            await _context.SaveChangesAsync();
            return departmentWithId;
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
}
