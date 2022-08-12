using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDevelopment.Common.Requests.SalaryRange;
using WebDevelopment.Domain;
using WebDevelopment.Domain.SalaryRange.Services;

namespace WebDevelopment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SalaryRangeController : ControllerBase
{
    private readonly ISalaryRangeService _salaryRangeService;

    public SalaryRangeController(ISalaryRangeService salaryRangeService)
    {
        _salaryRangeService = salaryRangeService;
    }

    [HttpGet()]
    public async Task<ActionResult> GetAllSalaryRanges()
    {
        try
        {
            var result = await _salaryRangeService.GetAllAsync();
            return Ok(new ResponseWrapper<IEnumerable<SalaryRangeWithIdRequest>>()
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
                    new Error()
                    {
                        Message = ex.Message
                    }
                }
            });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetSalaryRangeById(int id)
    {
        try
        {
            var result = await _salaryRangeService.GetById(id);
            return Ok(new ResponseWrapper<SalaryRangeWithIdRequest>()
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
                    new Error()
                    {
                        Message = ex.Message
                    }
                }
            });
        }
    }

    [HttpGet("{positionName}")]
    public async Task<ActionResult> GetSalaryRangeById(string positionName)
    {
        try
        {
            var result = await _salaryRangeService.GetByPositionName(positionName);
            return Ok(new ResponseWrapper<IEnumerable<SalaryRangeWithIdRequest>>()
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
                    new Error()
                    {
                        Message = ex.Message
                    }
                }
            });
        }
    }


    [HttpPost()]
    public async Task<ActionResult> AddSalaryRange([FromBody] NewSalaryRangeRequest salaryRange)
    {
        try
        {
            var result = await _salaryRangeService.AddNewSalaryRangeAsync(salaryRange);
            return Ok(new ResponseWrapper<object>()
            {
                Result = result
            });
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status403Forbidden, new ResponseWrapper<object>()
            {
                Errors = new List<Error>()
                {
                    new Error()
                    {
                        Message = e.Message
                    }
                }
            });
        }
    }

    [HttpPut()]
    public async Task<ActionResult> UpdateSalaryRange([FromBody] SalaryRangeWithIdRequest salaryRange)
    {
        try
        {
            var result = await _salaryRangeService.UpdateSalaryRangeAsync(salaryRange);
            return Ok(new ResponseWrapper<object>()
            {
                Result = result
            });
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status403Forbidden, new ResponseWrapper<object>()
            {
                Errors = new List<Error>()
                {
                    new Error()
                    {
                        Message = e.Message
                    }
                }
            });
        }
    }
}

