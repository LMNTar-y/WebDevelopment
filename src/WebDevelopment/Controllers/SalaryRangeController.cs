using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebDevelopment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SalaryRangeController : ControllerBase
{
    [HttpGet()]
    public ActionResult GetAllSalaryRanges()
    {
        return Ok();
    }

    [HttpGet("{id:int}")]
    public ActionResult GetSalaryRangeById(int id)
    {
        return Ok();
    }


    [HttpPost()]
    public ActionResult AddSalaryRange([FromBody] object o)
    {
        return Ok();
    }

    [HttpPut()]
    public ActionResult UpdateSalaryRange([FromBody] object o)
    {
        return Ok();
    }
}
