using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebDevelopment.API.Security.LoginModel;
using WebDevelopment.Infrastructure.Models.Auth;
using WebDevelopment.Infrastructure.Repos;

namespace WebDevelopment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "ApiKey")]
public class LoginController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly AuthUserModelRepo _userRepo;

    public LoginController(IConfiguration configuration, AuthUserModelRepo userRepo)
    {
        _configuration = configuration;
        _userRepo = userRepo;
    }

    //[AllowAnonymous]
    [HttpPost]
    public IActionResult Login([FromBody] UserLogin userLogin)
    {
        var user = Authenticate(userLogin);

        if (user != null)
        {
            var token = Generate(user);
            return Ok(token);
        }

        return NotFound("User not found");
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

    private AuthUserModel? Authenticate(UserLogin userLogin)
    {
        var user = _userRepo.GetByNameAndPassword(userLogin.UserName!, userLogin.Password!);
        return user;
    }
}