using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDevelopment.API.Services;
using WebDevelopment.Common.Requests.LoginModel;
using WebDevelopment.Domain;

namespace WebDevelopment.API.Controllers;

/// <summary>
/// The only controller in v1. To find other please choose v2.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
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

    /// <summary>
    /// It is used for getting bearer token for login in other controllers 
    /// </summary>
    /// <remarks>It is user for getting bearer token for login in other controllers </remarks>
    /// <param name="userLogin">UserName and Password</param>
    /// <response code="200">Ok. Token</response>
    /// <response code="400">Incorrect request</response>
    /// <response code="404">User not found</response>
    /// <returns>Bearer token</returns>
    [AllowAnonymous]
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

    /// <summary>
    /// Register a new user
    /// </summary>
    /// <remarks>Register a new user</remarks>
    /// <param name="userLogin">UserName, Password and email</param>
    /// <response code="201">Created</response>
    /// <response code="400">Incorrect request</response>
    /// <returns>Bearer token</returns>
    [ProducesResponseType(201)]
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] NewUserLogin userLogin)
    {
        try
        {
            var result = await _loginService.RegisterAsync(userLogin);
            return StatusCode(StatusCodes.Status201Created, new ResponseWrapper<object>()
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