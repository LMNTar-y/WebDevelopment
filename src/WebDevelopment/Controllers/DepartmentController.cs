using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebDevelopment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DepartmentController : ControllerBase
{
    [HttpGet()]
    public ActionResult GetAllDepartments()
    {
        return Ok();
    }

    [HttpGet("{id:int}")]
    public ActionResult GetDepartmentById(int id)
    {
        return Ok();
    }

    [HttpGet("{name}")]
    public ActionResult GetDepartmentByName(string name)
    {
        return Ok();
    }

    [HttpPost()]
    public ActionResult AddDepartment([FromBody] object o)
    {
        return Ok();
    }

    [HttpPut()]
    public ActionResult UpdateDepartment([FromBody] object o)
    {
        return Ok();
    }
}