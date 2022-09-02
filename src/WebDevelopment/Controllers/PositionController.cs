using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDevelopment.Common.Requests.Position;
using WebDevelopment.Domain;

namespace WebDevelopment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PositionController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public PositionController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet()]
    public async Task<ActionResult> GetAllPositions()
    {
        try
        {
            var result = await _unitOfWork.PositionRepo.GetAllAsync();
            return Ok(new ResponseWrapper<IEnumerable<PositionWithIdRequest>>()
            {
                Result = result.Cast<PositionWithIdRequest>()
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
            var result = await _unitOfWork.PositionRepo.GetByIdAsync(id);
            return Ok(new ResponseWrapper<PositionWithIdRequest>()
            {
                Result = (PositionWithIdRequest)result
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new ResponseWrapper<object>()
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
            var result = await _unitOfWork.PositionRepo.GetByNameAsync(name);
            return Ok(new ResponseWrapper<PositionWithIdRequest>()
            {
                Result = (PositionWithIdRequest)result
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
            var result = await _unitOfWork.PositionRepo.AddAsync(position);
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
    public async Task<ActionResult> UpdatePosition([FromBody] PositionWithIdRequest position)
    {
        try
        {
            var result = await _unitOfWork.PositionRepo.UpdateAsync(position);
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