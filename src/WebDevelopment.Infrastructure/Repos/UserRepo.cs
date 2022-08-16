using WebDevelopment.Common.Requests.User;
using WebDevelopment.Domain.IRepos;
using WebDevelopment.Infrastructure.Models;

namespace WebDevelopment.Infrastructure.Repos;

public class UserRepo : GenericRepository<User>, IUserRepo
{
    public UserRepo(WebDevelopmentContext context) : base(context)
    {
    }

    public new async Task<IEnumerable<IUserRequest>> GetAllAsync(string includeProperties = "")
    {
        var result = await base.GetAllAsync(includeProperties);
        return result.Select(Map);
    }

    public new async Task<IUserRequest> GetByIdAsync(object id)
    {
        var result = await base.GetByIdAsync(id);
        return Map(result);
    }

    public async Task<IUserRequest> GetUserByEmail(string email)
    {
        var user = (await base.GetAllAsync()).First(x => string.Equals(x.UserEmail, email, StringComparison.CurrentCultureIgnoreCase));

        return Map(user);
    }

    public async Task<IUserRequest> AddAsync(IUserRequest entity)
    {
        var result = Map((NewUserRequest)entity);
        await AddAsync(result);
        return entity;
    }

    public async Task<IUserRequest> UpdateAsync(IUserRequest entity)
    {
        var result = Map((UserWithIdRequest)entity);
        await UpdateAsync(result);
        return entity;
    }

    #region Mappers
    private static User Map(NewUserRequest userRequest)
    {
        return new User
        {
            FirstName = userRequest.FirstName,
            SecondName = userRequest.SecondName,
            UserEmail = userRequest.UserEmail
        };
    }

    private static User Map(UserWithIdRequest userWithIdRequest)
    {
        return new User
        {
            Id = userWithIdRequest.Id,
            FirstName = userWithIdRequest.FirstName,
            SecondName = userWithIdRequest.SecondName,
            UserEmail = userWithIdRequest.UserEmail,
            Active = userWithIdRequest.Active
        };
    }

    private static UserWithIdRequest Map(User user)
    {
        return new UserWithIdRequest
        {
            Id = user.Id,
            FirstName = user.FirstName,
            SecondName = user.SecondName,
            UserEmail = user.UserEmail,
            Active = user.Active
        };
    }

    #endregion
}