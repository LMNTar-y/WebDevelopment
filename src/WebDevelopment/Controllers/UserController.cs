using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDevelopment.Common.Requests.User;
using WebDevelopment.Domain;
using WebDevelopment.Domain.User.Services;

namespace WebDevelopment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var result = await _userService.GetAllUsers();
            return Ok(new ResponseWrapper<IEnumerable<UserWithIdRequest>>()
            {
                Result = result
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseWrapper<object>
            {
                Errors = new List<Error>()
                {
                    new Error{ Message = ex.Message}
                }
            });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        try
        {
            var result = await _userService.GetUserById(id);
            return Ok(new ResponseWrapper<UserWithIdRequest>()
            {
                Result = result
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new ResponseWrapper<object>
            {
                Errors = new List<Error>()
                {
                    new Error{ Message = ex.Message}
                }
            });
        }
    }

    [HttpGet("{userEmail}")]
    public async Task<IActionResult> GetUserByEmail(string userEmail)
    {
        try
        {
            var result = await _userService.GetUserByEmail(userEmail);
            return Ok(new ResponseWrapper<UserWithIdRequest>()
            {
                Result = result
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new ResponseWrapper<object>
            {
                Errors = new List<Error>()
                {
                    new Error{ Message = ex.Message}
                }
            });
        }
    }

    [HttpPost]
    public async Task<ActionResult> SaveAsync([FromBody] NewUserRequest userRequest)
    {
        try
        {
            var result = await _userService.CreateNewUserAsync(userRequest);
            return Ok(new ResponseWrapper<object>()
            {
                Result = result
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status403Forbidden, new ResponseWrapper<object>
            {
                Errors = new List<Error>()
                {
                    new Error{ Message = ex.Message}
                }
            });
        }
    }

    [HttpPut]
    public async Task<ActionResult> UpdateAsync([FromBody] UserWithIdRequest userWithIdRequest)
    {
        try
        {
            var result = await _userService.UpdateUserAsync(userWithIdRequest);
            return Ok(new ResponseWrapper<object>()
            {
                Result = result
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status403Forbidden, new ResponseWrapper<object>
            {
                Errors = new List<Error>()
                {
                    new Error{ Message = ex.Message}
                }
            });
        }
    }
}