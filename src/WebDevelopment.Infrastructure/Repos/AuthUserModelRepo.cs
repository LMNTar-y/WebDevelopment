using Microsoft.EntityFrameworkCore;
using WebDevelopment.Infrastructure.Models.Auth;

namespace WebDevelopment.Infrastructure.Repos;

public class AuthUserModelRepo : IAuthUserModelRepo
{
    private readonly WebDevelopmentContext _context;

    public AuthUserModelRepo(WebDevelopmentContext context)
    {
        _context = context;
    }

    public async Task<AuthUserModel> GetByNameAsync(string login)
    {
        var authUser = await 
            _context.AuthUserModels.Include(u => u.User).SingleOrDefaultAsync(x => string.Equals(x.UserName, login));
        return authUser ?? throw new ArgumentNullException(nameof(login), "Incorrect login or password");
    }

    public async Task<AuthUserModel> AddAsync(AuthUserModel authUserModel)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => string.Equals(u.UserEmail, authUserModel.UserEmail));
        authUserModel.User = user;
        _context.AuthUserModels.Add(authUserModel);
        await _context.SaveChangesAsync();
        return authUserModel;
    }
}

public interface IAuthUserModelRepo
{
    Task<AuthUserModel> GetByNameAsync(string login);
    Task<AuthUserModel> AddAsync(AuthUserModel authUserModel);

}