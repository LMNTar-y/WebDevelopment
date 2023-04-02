using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDevelopment.Common.Requests.UserSalary;
using WebDevelopment.Domain;

namespace WebDevelopment.API.Controllers;

[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UsersSalaryController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    public UsersSalaryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet()]
    public async Task<ActionResult> GetAllUsersSalaryPositions()
    {
        try
        {
            var result = await _unitOfWork.UserSalaryRepo.GetAllAsync();
            return Ok(new ResponseWrapper<IEnumerable<UserSalaryWithIdRequest>>()
            {
                Result = result.Cast<UserSalaryWithIdRequest>()
            });
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseWrapper<object>()
            {
                Errors = new List<Error>()
                {
                    new()
                    {
                        Message = e.Message
                    }
                }
            });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetUsersSalaryById(int id)
    {
        try
        {
            var result = await _unitOfWork.UserSalaryRepo.GetByIdAsync(id);
            return Ok(new ResponseWrapper<UserSalaryWithIdRequest>()
            {
                Result = (UserSalaryWithIdRequest)result
            });
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseWrapper<object>()
            {
                Errors = new List<Error>()
                {
                    new()
                    {
                        Message = e.Message
                    }
                }
            });
        }
    }

    [HttpPost()]
    public async Task<ActionResult> AddUsersSalary([FromBody] NewUserSalaryRequest userSalaryRequest)
    {
        try
        {
            var result = await _unitOfWork.UserSalaryRepo.AddAsync(userSalaryRequest);
            return StatusCode(StatusCodes.Status201Created, new ResponseWrapper<object>()
            {
                Result = result
            });
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseWrapper<object>()
            {
                Errors = new List<Error>()
                {
                    new()
                    {
                        Message = e.Message
                    }
                }
            });
        }
    }

    [HttpPut()]
    public async Task<ActionResult> UpdateUsersSalary([FromBody] UserSalaryWithIdRequest userSalaryRequest)
    {
        try
        {
            var result = await _unitOfWork.UserSalaryRepo.UpdateAsync(userSalaryRequest);
            return Ok(new ResponseWrapper<object>()
            {
                Result = result
            });
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseWrapper<object>()
            {
                Errors = new List<Error>()
                {
                    new()
                    {
                        Message = e.Message
                    }
                }
            });
        }
    }
}