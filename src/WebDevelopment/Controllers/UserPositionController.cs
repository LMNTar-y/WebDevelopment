using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDevelopment.Common.Requests.UserPosition;
using WebDevelopment.Domain;
using WebDevelopment.Domain.UserPosition.Services;

namespace WebDevelopment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserPositionController : ControllerBase
{
    private readonly IUserPositionService _userPositionService;

    public UserPositionController(IUserPositionService userPositionService)
    {
        _userPositionService = userPositionService;
    }

    [HttpGet()]
    public async Task<ActionResult> GetAllUserPositions()
    {
        try
        {
            var result = await _userPositionService.GetAllAsync();
            return Ok(new ResponseWrapper<IEnumerable<UserPositionWithIdRequest>>()
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
    public async Task<ActionResult> GetUserPositionById(int id)
    {
        try
        {
            var result = await _userPositionService.GetById(id);
            return Ok(new ResponseWrapper<UserPositionWithIdRequest>()
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

    [HttpGet("{firstName}/{secondName}")]
    public async Task<ActionResult> GetUserPositionByUserName(string firstName, string secondName)
    {
        try
        {
            var result = await _userPositionService.GetByUserNameAsync(firstName, secondName);
            return Ok(new ResponseWrapper<IEnumerable<UserPositionWithIdRequest>>()
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
    public async Task<ActionResult> AddUserPosition([FromBody] NewUserPositionRequest userPosition)
    {
        try
        {
            var result = await _userPositionService.AddNewUserPositionAsync(userPosition);
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


    [HttpPut()]
    public async Task<ActionResult> UpdateUserPosition([FromBody] UserPositionWithIdRequest userPosition)
    {
        try
        {
            var result = await _userPositionService.UpdateUserPositionAsync(userPosition);
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