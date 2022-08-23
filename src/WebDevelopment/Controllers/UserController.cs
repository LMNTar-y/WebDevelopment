using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDevelopment.Common.Requests.User;
using WebDevelopment.Domain;

namespace WebDevelopment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
    public async Task<ActionResult> SaveAsync([FromBody] NewUserRequest userRequest)
    {
        try
        {
            var result = await _unitOfWork.UserRepo.AddAsync(userRequest);
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