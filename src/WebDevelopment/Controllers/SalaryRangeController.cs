using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDevelopment.Common.Requests.SalaryRange;
using WebDevelopment.Domain;

namespace WebDevelopment.API.Controllers;

[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class SalaryRangeController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;


    public SalaryRangeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet()]
    public async Task<ActionResult> GetAllSalaryRanges()
    {
        try
        {
            var result = await _unitOfWork.SalaryRangeRepo.GetAllAsync();
            return Ok(new ResponseWrapper<IEnumerable<SalaryRangeWithIdRequest>>()
            {
                Result = result.Cast<SalaryRangeWithIdRequest>()
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
            var result = await _unitOfWork.SalaryRangeRepo.GetByIdAsync(id);
            return Ok(new ResponseWrapper<SalaryRangeWithIdRequest>()
            {
                Result = (SalaryRangeWithIdRequest)result
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
    public async Task<ActionResult> GetByPositionName(string positionName)
    {
        try
        {
            var result = await _unitOfWork.SalaryRangeRepo.GetByNameAsync(positionName);
            return Ok(new ResponseWrapper<IEnumerable<SalaryRangeWithIdRequest>>()
            {
                Result = result.Cast<SalaryRangeWithIdRequest>()
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
            var result = await _unitOfWork.SalaryRangeRepo.AddAsync(salaryRange);
            return StatusCode(StatusCodes.Status201Created, new ResponseWrapper<object>()
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
            var result = await _unitOfWork.SalaryRangeRepo.UpdateAsync(salaryRange);
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

