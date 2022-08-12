using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDevelopment.Common.Requests.UserTask;
using WebDevelopment.Domain;
using WebDevelopment.Domain.UserTask.Services;

namespace WebDevelopment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserTaskController : ControllerBase
{
    private readonly IUserTaskService _userTaskService;


    public UserTaskController(IUserTaskService userTaskService)
    {
        _userTaskService = userTaskService;
    }

    [HttpGet()]
    public async Task<ActionResult> GetAllUsersTasksPositions()
    {
        try
        {
            var result = await _userTaskService.GetAllAsync();
            return Ok(new ResponseWrapper<IEnumerable<UserTaskWithIdRequest>>()
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

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetUsersSalaryById(int id)
    {
        try
        {
            var result = await _userTaskService.GetById(id);
            return Ok(new ResponseWrapper<UserTaskWithIdRequest>()
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

    [HttpPost()]
    public async Task<ActionResult> AddUsersSalary([FromBody] NewUserTaskRequest userSalaryRequest)
    {
        try
        {
            var result = await _userTaskService.AddNewUseTaskAsync(userSalaryRequest);
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

    [HttpPut()]
    public async Task<ActionResult> UpdateUsersSalary([FromBody] UserTaskWithIdRequest userSalaryRequest)
    {
        try
        {
            var result = await _userTaskService.UpdateUserTaskAsync(userSalaryRequest);
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