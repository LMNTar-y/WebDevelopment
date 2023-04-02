using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDevelopment.Common.Requests.Task;
using WebDevelopment.Domain;

namespace WebDevelopment.API.Controllers;

[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TaskController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public TaskController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet()]
    public async Task<ActionResult> GetAllPositions()
    {
        try
        {
            var result = await _unitOfWork.TaskRepo.GetAllAsync();
            return Ok(new ResponseWrapper<IEnumerable<TaskWithIdRequest>>()
            {
                Result = result.Cast<TaskWithIdRequest>()
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
            var result = await _unitOfWork.TaskRepo.GetByIdAsync(id);
            return Ok(new ResponseWrapper<TaskWithIdRequest>()
            {
                Result = (TaskWithIdRequest)result
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
            var result = await _unitOfWork.TaskRepo.GetByNameAsync(name);
            return Ok(new ResponseWrapper<TaskWithIdRequest>()
            {
                Result = (TaskWithIdRequest)result
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
            var result = await _unitOfWork.TaskRepo.AddAsync(task);
            return StatusCode(StatusCodes.Status201Created, new ResponseWrapper<object>()
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
            var result = await _unitOfWork.TaskRepo.UpdateAsync(task);
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