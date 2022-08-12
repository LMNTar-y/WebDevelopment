﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDevelopment.Common.Requests.UserSalary;
using WebDevelopment.Domain;
using WebDevelopment.Domain.UserSalary.Services;

namespace WebDevelopment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersSalaryController : ControllerBase
{
    private readonly IUserSalaryService _userSalaryService;

    public UsersSalaryController(IUserSalaryService userSalaryService)
    {
        _userSalaryService = userSalaryService;
    }

    [HttpGet()]
    public async Task<ActionResult> GetAllUsersSalaryPositions()
    {
        try
        {
            var result = await _userSalaryService.GetAllAsync();
            return Ok(new ResponseWrapper<IEnumerable<UserSalaryWithIdRequest>>()
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
            var result = await _userSalaryService.GetById(id);
            return Ok(new ResponseWrapper<UserSalaryWithIdRequest>()
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
    public async Task<ActionResult> AddUsersSalary([FromBody] NewUserSalaryRequest userSalaryRequest)
    {
        try
        {
            var result = await _userSalaryService.AddNewUserSalaryAsync(userSalaryRequest);
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
    public async Task<ActionResult> UpdateUsersSalary([FromBody] UserSalaryWithIdRequest userSalaryRequest)
    {
        try
        {
            var result = await _userSalaryService.UpdateUserSalaryAsync(userSalaryRequest);
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