
using WebDevelopment.Common.Requests.User;
using WebDevelopment.Domain;
using WebDevelopment.Infrastructure.Models;

namespace WebDevelopment.Infrastructure;

public class TestUserRepo : GenericRepository<User>, ITestUserRepo
{
    public TestUserRepo(WebDevelopmentContext context) : base(context)
    {
    }

    public new async Task<IEnumerable<IUserRequest>> GetAll()
    {
        var result = await base.GetAll();
        return result.Select(Map);
    }

    public Task<IUserRequest> Add(IUserRequest entity)
    {
        throw new NotImplementedException();
    }

    public Task<IUserRequest> Update(IUserRequest entity)
    {
        throw new NotImplementedException();
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