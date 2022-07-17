using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebDevelopment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PositionController : ControllerBase
{
    [HttpGet()]
    public ActionResult GetAllPositions()
    {
        return Ok();
    }

    [HttpGet("{id:int}")]
    public ActionResult GetPositionById(int id)
    {
        return Ok();
    }

    [HttpGet("{name}")]
    public ActionResult GetPositionByName(string name)
    {
        return Ok();
    }

    [HttpPost()]
    public ActionResult AddPosition([FromBody] object o)
    {
        return Ok();
    }

    [HttpPut()]
    public ActionResult UpdatePosition([FromBody] object o)
    {
        return Ok();
    }
}