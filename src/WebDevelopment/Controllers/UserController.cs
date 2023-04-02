using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebDevelopment.Common.Requests.User;
using WebDevelopment.Domain;

namespace WebDevelopment.API.Controllers;

[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
public class UserController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public UserController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        // or just like this - if (string.Equals(User.FindFirst(ClaimTypes.Role)?.Value, "User", StringComparison.Ordinal))
        if (HttpContext.User.Identity is ClaimsIdentity identity && string.Equals(identity.FindFirst(ClaimTypes.Role)?.Value, "User", StringComparison.Ordinal))
        {
            int.TryParse(identity.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int id);
            return await GetById(id);
        }

        try
        {
            var result = await _unitOfWork.UserRepo.GetAllAsync();
            return Ok(new ResponseWrapper<IEnumerable<UserWithIdRequest>>()
            {
                Result = result.Cast<UserWithIdRequest>()
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
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await _unitOfWork.UserRepo.GetByIdAsync(id);
            return Ok(new ResponseWrapper<UserWithIdRequest>()
            {
                Result = (UserWithIdRequest)result
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
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUserByEmail(string userEmail)
    {
        try
        {
            var result = await _unitOfWork.UserRepo.GetUserByEmail(userEmail);
            return Ok(new ResponseWrapper<UserWithIdRequest>()
            {
                Result = (UserWithIdRequest)result
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
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> SaveAsync([FromBody] NewUserRequest userRequest)
    {
        try
        {
            var result = await _unitOfWork.UserRepo.AddAsync(userRequest);
            return StatusCode(StatusCodes.Status201Created, new ResponseWrapper<object>()
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
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> UpdateAsync([FromBody] UserWithIdRequest userWithIdRequest)
    {
        try
        {
            var result = await _unitOfWork.UserRepo.UpdateAsync(userWithIdRequest);
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