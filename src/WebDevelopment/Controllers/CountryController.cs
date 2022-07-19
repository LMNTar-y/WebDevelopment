using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebDevelopment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CountryController : ControllerBase
    {
        [HttpGet()]
        public ActionResult GetAllCountries()
        {
            return Ok();
        }

        [HttpGet("{id:int}")]
        public ActionResult GetCountryById(int id)
        {
            return Ok();
        }

        [HttpGet("{name}")]
        public ActionResult GetCountryByName(string name)
        {
            return Ok();
        }

        [HttpPost()]
        public ActionResult AddCountry([FromBody] object o)
        {
            return Ok();
        }

        [HttpPut()]
        public ActionResult UpdateCountry([FromBody] object o)
        {
            return Ok();
        }
    }
}
