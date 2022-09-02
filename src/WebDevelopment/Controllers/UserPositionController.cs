using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDevelopment.Common.Requests.UserPosition;
using WebDevelopment.Domain;

namespace WebDevelopment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UserPositionController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;


    public UserPositionController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet()]
    public async Task<ActionResult> GetAllUserPositions()
    {
        try
        {
            var result = await _unitOfWork.UserPositionRepo.GetAllAsync();
            return Ok(new ResponseWrapper<IEnumerable<UserPositionWithIdRequest>>()
            {
                Result = result.Cast<UserPositionWithIdRequest>()
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
    public async Task<ActionResult> GetUserPositionById(int id)
    {
        try
        {
            var result = await _unitOfWork.UserPositionRepo.GetByIdAsync(id);
            return Ok(new ResponseWrapper<UserPositionWithIdRequest>()
            {
                Result = (UserPositionWithIdRequest)result
            });
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status404NotFound, new ResponseWrapper<object>()
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

    [HttpGet("{firstName}/{secondName}")]
    public async Task<ActionResult> GetUserPositionByUserName(string firstName, string secondName)
    {
        try
        {
            var result = await _unitOfWork.UserPositionRepo.GetByUserNameAsync(firstName, secondName);
            return Ok(new ResponseWrapper<IEnumerable<UserPositionWithIdRequest>>()
            {
                Result = result.Cast<UserPositionWithIdRequest>()
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
    public async Task<ActionResult> AddUserPosition([FromBody] NewUserPositionRequest userPosition)
    {
        try
        {
            var result = await _unitOfWork.UserPositionRepo.AddAsync(userPosition);
            return StatusCode(StatusCodes.Status201Created, new ResponseWrapper<object>()
            {
                Result = result
            });
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status404NotFound, new ResponseWrapper<object>()
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
    public async Task<ActionResult> UpdateUserPosition([FromBody] UserPositionWithIdRequest userPosition)
    {
        try
        {
            var result = await _unitOfWork.UserPositionRepo.UpdateAsync(userPosition);
            return Ok(new ResponseWrapper<object>()
            {
                Result = result
            });
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status404NotFound, new ResponseWrapper<object>()
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