using Microsoft.EntityFrameworkCore;
using WebDevelopment.Common.Requests.User;
using WebDevelopment.Common.Requests.UserSalary;
using WebDevelopment.Domain.UserSalary;
using WebDevelopment.Infrastructure.Models;

namespace WebDevelopment.Infrastructure.Repositories;

public class UserSalaryRepository : IUserSalaryRepository
{
    private readonly WebDevelopmentContext _context;

    public UserSalaryRepository(WebDevelopmentContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserSalaryWithIdRequest>> GetAll()
    {
        var result = await _context.UsersSalaries.Include(u => u.User).ToListAsync();
        return result.Select(Map);
    }

    public async Task<NewUserSalaryRequest> Add(NewUserSalaryRequest item)
    {
        var result = Map(item);
        var user = _context.Users.First(x => string.Equals(item.User.FirstName.ToLower(), x.FirstName.ToLower()) &&
                                             string.Equals(item.User.SecondName.ToLower(), x.SecondName.ToLower()) &&
                                             string.Equals(item.User.UserEmail.ToLower(), x.UserEmail.ToLower()));
        result.User = user;
        _context.UsersSalaries.Add(result);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<UserSalaryWithIdRequest> Update(UserSalaryWithIdRequest itemWithId)
    {
        var result = Map(itemWithId);
        var user = _context.Users.First(x => string.Equals(itemWithId.User.FirstName.ToLower(), x.FirstName.ToLower()) &&
                                             string.Equals(itemWithId.User.SecondName.ToLower(), x.SecondName.ToLower()) &&
                                             string.Equals(itemWithId.User.UserEmail.ToLower(), x.UserEmail.ToLower()));
        result.User = user;
        _context.UsersSalaries.Update(result);
        await _context.SaveChangesAsync();
        return itemWithId;
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