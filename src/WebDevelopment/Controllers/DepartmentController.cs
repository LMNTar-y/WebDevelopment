using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDevelopment.Common.Requests.Department;
using WebDevelopment.Domain;

namespace WebDevelopment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class DepartmentController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;


    public DepartmentController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllDepartments()
    {
        try
        {
            var result = await _unitOfWork.DepartmentRepo.GetAllAsync();
            return Ok(new ResponseWrapper<IEnumerable<DepartmentWithIdRequest>>()
            {
                Result = result.Cast<DepartmentWithIdRequest>()
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
            var result = await _unitOfWork.DepartmentRepo.GetByIdAsync(id);
            return Ok(new ResponseWrapper<DepartmentWithIdRequest>()
            {
                Result = (DepartmentWithIdRequest)result
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
            var result = await _unitOfWork.DepartmentRepo.GetByNameAsync(name);
            return Ok(new ResponseWrapper<DepartmentWithIdRequest>()
            {
                Result = (DepartmentWithIdRequest)result
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
            var result = await _unitOfWork.DepartmentRepo.AddAsync(department);
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
            var result = await _unitOfWork.DepartmentRepo.UpdateAsync(department);
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
