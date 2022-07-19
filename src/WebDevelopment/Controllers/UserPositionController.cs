using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebDevelopment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserPositionController : ControllerBase
{
    [HttpGet()]
    public ActionResult GetAllUserPositions()
    {
        return Ok();
    }

    [HttpGet("{id:int}")]
    public ActionResult GetUserPositionById(int id)
    {
        return Ok();
    }

    [HttpPost()]
    public ActionResult AddUserPosition([FromBody] object o)
    {
        return Ok();
    }

    [HttpPut()]
    public ActionResult UpdateUserPosition([FromBody] object o)
    {
        return Ok();
    }
}