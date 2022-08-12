using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDevelopment.Common.Requests.Task;
using WebDevelopment.Domain;
using WebDevelopment.Domain.Task.Services;

namespace WebDevelopment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet()]
    public async Task<ActionResult> GetAllPositions()
    {
        try
        {
            var result = await _taskService.GetAllAsync();
            return Ok(new ResponseWrapper<IEnumerable<TaskWithIdRequest>>()
            {
                Result = result
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseWrapper<object>()
            {
                Errors = new List<Error>()
                {
                    new Error{Message = ex.Message}
                }
            });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetPositionById(int id)
    {
        try
        {
            var result = await _taskService.GetById(id);
            return Ok(new ResponseWrapper<TaskWithIdRequest>()
            {
                Result = result
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseWrapper<object>()
            {
                Errors = new List<Error>()
                {
                    new Error{Message = ex.Message}
                }
            });
        }
    }

    [HttpGet("{name}")]
    public async Task<ActionResult> GetPositionByName(string name)
    {
        try
        {
            var result = await _taskService.GetByName(name);
            return Ok(new ResponseWrapper<TaskWithIdRequest>()
            {
                Result = result
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseWrapper<object>()
            {
                Errors = new List<Error>()
                {
                    new Error{Message = ex.Message}
                }
            });
        }
    }

    [HttpPost()]
    public async Task<ActionResult> AddPosition([FromBody] NewTaskRequest task)
    {
        try
        {
            var result = await _taskService.AddNewTaskAsync(task);
            return Ok(new ResponseWrapper<object>()
            {
                Result = result
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseWrapper<object>()
            {
                Errors = new List<Error>()
                {
                    new Error{Message = ex.Message}
                }
            });
        }
    }

    [HttpPut()]
    public async Task<ActionResult> UpdatePosition([FromBody] TaskWithIdRequest task)
    {
        try
        {
            var result = await _taskService.UpdateTaskAsync(task);
            return Ok(new ResponseWrapper<object>()
            {
                Result = result
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseWrapper<object>()
            {
                Errors = new List<Error>()
                {
                    new Error{Message = ex.Message}
                }
            });
        }
    }
}