using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDevelopment.API.Services;
using WebDevelopment.Common.Requests.LoginModel;
using WebDevelopment.Domain;

namespace WebDevelopment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "ApiKey")]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    //[AllowAnonymous]
    [HttpPost()]
    public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
    {
        try
        {
            var token = await _loginService.LoginAsync(userLogin);
            return Ok(new ResponseWrapper<string>()
            {
                Result = token
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new ResponseWrapper<object>()
            {
                Errors = new List<Error>()
                {
                    new Error{ Message = ex.Message}
                }
            });
        }
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] NewUserLogin userLogin)
    {
        try
        {
            var result = await _loginService.RegisterAsync(userLogin);
            return Ok(new ResponseWrapper<object>()
            {
                Result = result
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new ResponseWrapper<object>()
            {
                Errors = new List<Error>()
                {
                    new Error{ Message = ex.Message}
                }
            });
        }
    }
}