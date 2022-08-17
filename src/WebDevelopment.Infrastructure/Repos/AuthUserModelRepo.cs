using Microsoft.EntityFrameworkCore;
using WebDevelopment.Infrastructure.Models.Auth;

namespace WebDevelopment.Infrastructure.Repos;

public class AuthUserModelRepo
{
    private readonly WebDevelopmentContext _context;

    public AuthUserModelRepo(WebDevelopmentContext context)
    {
        _context = context;
    }

    public AuthUserModel? GetByNameAndPassword(string name, string password)
    {
        var authUser =
            _context.AuthUserModels.Include(u => u.User).SingleOrDefault(x => string.Equals(x.UserName, name) && string.Equals(x.Password, password));
        return authUser;
    }
}