using WebDevelopment.Common.Requests.UserSalary;

namespace WebDevelopment.Domain.UserSalary;

public interface IUserSalaryRepository : IDefaultRepository<UserSalaryWithIdRequest, NewUserSalaryRequest>
{
    
}