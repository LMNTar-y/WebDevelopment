using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDevelopment.Common.Requests.UserTask;
using WebDevelopment.Domain;

namespace WebDevelopment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UserTaskController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public UserTaskController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet()]
    public async Task<ActionResult> GetAllUsersTasksPositions()
    {
        try
        {
            var result = await _unitOfWork.UserTaskRepo.GetAllAsync();
            return Ok(new ResponseWrapper<IEnumerable<UserTaskWithIdRequest>>()
            {
                Result = result.Cast<UserTaskWithIdRequest>()
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
            var result = await _unitOfWork.UserTaskRepo.GetByIdAsync(id);
            return Ok(new ResponseWrapper<UserTaskWithIdRequest>()
            {
                Result = (UserTaskWithIdRequest)result
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
    public async Task<ActionResult> AddUsersSalary([FromBody] NewUserTaskRequest userSalaryRequest)
    {
        try
        {
            var result = await _unitOfWork.UserTaskRepo.AddAsync(userSalaryRequest);
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
    public async Task<ActionResult> UpdateUsersSalary([FromBody] UserTaskWithIdRequest userSalaryRequest)
    {
        try
        {
            var result = await _unitOfWork.UserTaskRepo.UpdateAsync(userSalaryRequest);
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