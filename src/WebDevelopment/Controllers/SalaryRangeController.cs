﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDevelopment.Common.Requests.SalaryRange;
using WebDevelopment.Domain;

namespace WebDevelopment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
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
            return Ok(new ResponseWrapper<IEnumerable<ISalaryRangeRequest>>()
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
            var result = await _unitOfWork.SalaryRangeRepo.GetByIdAsync(id);
            return Ok(new ResponseWrapper<ISalaryRangeRequest>()
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
            var result = await _unitOfWork.SalaryRangeRepo.GetByPositionNameAsync(positionName);
            return Ok(new ResponseWrapper<IEnumerable<ISalaryRangeRequest>>()
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
            var result = await _unitOfWork.SalaryRangeRepo.AddAsync(salaryRange);
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

