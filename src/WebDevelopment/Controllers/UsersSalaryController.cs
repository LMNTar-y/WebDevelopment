using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebDevelopment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersSalaryController : ControllerBase
{
    [HttpGet()]
    public ActionResult GetAllUsersSalaryPositions()
    {
        return Ok();
    }

    [HttpGet("{id:int}")]
    public ActionResult GetUsersSalaryById(int id)
    {
        return Ok();
    }

    [HttpPost()]
    public ActionResult AddUsersSalary([FromBody] object o)
    {
        return Ok();
    }

    [HttpPut()]
    public ActionResult UpdateUsersSalary([FromBody] object o)
    {
        return Ok();
    }
}