using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDevelopment.Common.Requests.Department;
using WebDevelopment.Domain;
using WebDevelopment.Domain.Department.Services;

namespace WebDevelopment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllDepartments()
    {
        try
        {
            var result = await _departmentService.GetAllAsync();
            return Ok(new ResponseWrapper<IEnumerable<DepartmentWithIdRequest>>()
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

    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetDepartmentById(Guid id)
    {
        try
        {
            var result = await _departmentService.GetById(id);
            return Ok(new ResponseWrapper<DepartmentWithIdRequest>()
            {
                Result = result
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new ResponseWrapper<object>()
            {
                Errors = new List<Error>()
                {
                    new Error(){Message = ex.Message}
                }
            });
        }
    }

    [HttpGet("{name}")]
    public async Task<ActionResult> GetDepartmentByName(string name)
    {
        try
        {
            var result = await _departmentService.GetByName(name);
            return Ok(new ResponseWrapper<DepartmentWithIdRequest>()
            {
                Result = result
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status404NotFound, new ResponseWrapper<object>()
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
    public async Task<ActionResult> AddDepartment([FromBody] NewDepartmentRequest department)
    {
        try
        {
            var result = await _departmentService.AddNewDepartmentAsync(department);
            return Ok(new ResponseWrapper<object>()
            {
                Result = result
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status403Forbidden, new ResponseWrapper<object>()
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

    [HttpPut()]
    public async Task<ActionResult> UpdateDepartment([FromBody] DepartmentWithIdRequest department)
    {
        try
        {
            var result = await _departmentService.UpdateDepartmentAsync(department);
            return Ok(new ResponseWrapper<object>()
            {
                Result = result
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status403Forbidden, new ResponseWrapper<object>()
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
}
