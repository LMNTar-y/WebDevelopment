
using WebDevelopment.Common.Requests.User;
using WebDevelopment.Common.Requests.UserSalary;
using WebDevelopment.Domain.IRepos;
using WebDevelopment.Infrastructure.Models;

namespace WebDevelopment.Infrastructure.Repos;

public class UserSalaryRepo : GenericRepository<UsersSalary>, IUserSalaryRepo
{
    private readonly GenericRepository<User> _user;
    public UserSalaryRepo(WebDevelopmentContext context) : base(context)
    {
        _user = new GenericRepository<User>(context);
    }

    public new async Task<IEnumerable<IUserSalaryRequest>> GetAllAsync(string includeProperties = "")
    {
        includeProperties += "User";
        var result = await base.GetAllAsync(includeProperties);
        return result.Select(Map);
    }

    public new async Task<IUserSalaryRequest> GetByIdAsync(object id)
    {
        var includeProperties = "User";
        var result = (await base.GetAllAsync(includeProperties)).Single(x => x.Id == (int)id);
        return Map(result);
    }

    public async Task<IUserSalaryRequest> AddAsync(IUserSalaryRequest entity)
    {
        var result = Map((NewUserSalaryRequest)entity);
        var user = (await _user.GetAllAsync()).First(x => string.Equals(entity?.User?.FirstName?.ToLower(), x?.FirstName?.ToLower()) &&
                                             string.Equals(entity?.User?.SecondName?.ToLower(), x?.SecondName?.ToLower()) &&
                                             string.Equals(entity?.User?.UserEmail?.ToLower(), x?.UserEmail?.ToLower()));
        result.User = user;
        await AddAsync(result);
        return entity;
    }

    public async Task<IUserSalaryRequest> UpdateAsync(IUserSalaryRequest entity)
    {
        var result = Map((UserSalaryWithIdRequest)entity);
        var user = (await _user.GetAllAsync()).First(x => string.Equals(entity?.User?.FirstName?.ToLower(), x?.FirstName?.ToLower()) &&
                                                          string.Equals(entity?.User?.SecondName?.ToLower(), x?.SecondName?.ToLower()) &&
                                                          string.Equals(entity?.User?.UserEmail?.ToLower(), x?.UserEmail?.ToLower()));
        result.User = user;
        await UpdateAsync(result);
        return entity;
    }

    #region Mappers

    private static UserSalaryWithIdRequest Map(UsersSalary usersSalary)
    {
        return new UserSalaryWithIdRequest()
        {
            Id = usersSalary.Id,
            ChangeTime = usersSalary.ChangeTime,
            Salary = usersSalary.Salary,
            User = new NewUserRequest()
            {
                FirstName = usersSalary?.User?.FirstName,
                SecondName = usersSalary?.User?.SecondName,
                UserEmail = usersSalary?.User?.UserEmail
            }
        };
    }

    private static UsersSalary Map(NewUserSalaryRequest usersSalary)
    {
        return new UsersSalary()
        {
            ChangeTime = DateTimeOffset.Now,
            Salary = usersSalary.Salary,
        };
    }

    private static UsersSalary Map(UserSalaryWithIdRequest usersSalary)
    {
        return new UsersSalary()
        {
            Id = usersSalary.Id,
            ChangeTime = DateTimeOffset.Now,
            Salary = usersSalary.Salary,
        };
    }

    #endregion
}