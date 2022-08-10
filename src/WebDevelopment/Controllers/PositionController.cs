using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDevelopment.Common.Requests.Position;
using WebDevelopment.Domain;
using WebDevelopment.Domain.Position.Services;

namespace WebDevelopment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PositionController : ControllerBase
{
    private readonly IPositionService _positionService;

    public PositionController(IPositionService positionService)
    {
        _positionService = positionService;
    }

    [HttpGet()]
    public async Task<ActionResult> GetAllPositions()
    {
        try
        {
            var result = await _positionService.GetAllAsync();
            return Ok(new ResponseWrapper<IEnumerable<PositionWithIdRequest>>()
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
            var result = await _positionService.GetById(id);
            return Ok(new ResponseWrapper<PositionWithIdRequest>()
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
            var result = await _positionService.GetByName(name);
            return Ok(new ResponseWrapper<PositionWithIdRequest>()
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
    public async Task<ActionResult> AddPosition([FromBody] NewPositionRequest position)
    {
        try
        {
            var result = await _positionService.AddNewPositionAsync(position);
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
    public async Task<ActionResult> UpdatePosition([FromBody] PositionWithIdRequest position)
    {
        try
        {
            var result = await _positionService.UpdatePositionAsync(position);
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