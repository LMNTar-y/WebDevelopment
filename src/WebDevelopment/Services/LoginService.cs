using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebDevelopment.Common.Requests.LoginModel;
using WebDevelopment.Infrastructure.Models.Auth;
using WebDevelopment.Infrastructure.Repos;

namespace WebDevelopment.API.Services;

public class LoginService : ILoginService
{
    private readonly IConfiguration _configuration;
    private readonly IAuthUserModelRepo _userRepo;

    public LoginService(IConfiguration configuration, IAuthUserModelRepo userRepo)
    {
        _configuration = configuration;
        _userRepo = userRepo;
    }

    public async Task<string> LoginAsync(UserLogin userLogin)
    {
        var user = await Authenticate(userLogin);

        if (user == null)
        {
            throw new ArgumentNullException(nameof(userLogin), "Incorrect login or password");
        }

        var token = Generate(user);
        return token;
    }

    public async Task<bool> RegisterAsync(NewUserLogin userLogin)
    {
        var user = new AuthUserModel
        {
            SecuredPassword = BCrypt.Net.BCrypt.HashPassword(userLogin.Password),
            UserName = userLogin.UserName,
            UserEmail = userLogin.UserEmail,
            Role = Roles.User
        };

        await _userRepo.AddAsync(user);
        return true;
    }

    private string Generate(AuthUserModel user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserName!),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(ClaimTypes.Name, user.User!.FirstName!),
            new Claim(ClaimTypes.Surname, user.User.SecondName!),
            new Claim(ClaimTypes.Email, user.User.UserEmail!)
        };

        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<AuthUserModel?> Authenticate(UserLogin userLogin)
    {
        var user = await _userRepo.GetByNameAsync(userLogin.UserName!);
        bool isValidPassword = BCrypt.Net.BCrypt.Verify(userLogin.Password, user.SecuredPassword);

        if (isValidPassword)
        {
            return user;
        }

        return null;
    }
}