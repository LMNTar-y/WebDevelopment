using Microsoft.EntityFrameworkCore;
using WebDevelopment.Common.Requests.User;
using WebDevelopment.Domain.User;
using WebDevelopment.Infrastructure.Models;

namespace WebDevelopment.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly WebDevelopmentContext _context;

    public UserRepository(WebDevelopmentContext context)
    {
        _context = context ?? throw new ArgumentException($"{nameof(context)} was not downloaded from DI"); ;
    }

    public async Task<IEnumerable<UserWithIdRequest>> GetAll()
    {
        var result = await _context.Users.ToListAsync();
        return result.Select(Map);
    }

    public async Task<NewUserRequest> Add(NewUserRequest user)
    {
        var result = Map(user);
        _context.Users.Add(result);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<UserWithIdRequest> Update(UserWithIdRequest userWithId)
    {
        var result = Map(userWithId);
        _context.Users.Update(result);
        await _context.SaveChangesAsync();
        return userWithId;
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