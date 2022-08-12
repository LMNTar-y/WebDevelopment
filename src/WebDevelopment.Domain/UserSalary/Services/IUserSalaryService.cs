

using WebDevelopment.Common.Requests.UserSalary;

namespace WebDevelopment.Domain.UserSalary.Services;

public interface IUserSalaryService
{
    Task<IEnumerable<UserSalaryWithIdRequest>> GetAllAsync();

    Task<UserSalaryWithIdRequest> GetById(int id);
    
    Task<bool> AddNewUserSalaryAsync(NewUserSalaryRequest request);

    Task<bool> UpdateUserSalaryAsync(UserSalaryWithIdRequest requestWithId);
}