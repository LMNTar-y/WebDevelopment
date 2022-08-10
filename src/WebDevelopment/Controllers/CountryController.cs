using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDevelopment.Common.Requests.Country;
using WebDevelopment.Domain;
using WebDevelopment.Domain.Country.Services;

namespace WebDevelopment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet()]
        public async Task<ActionResult> GetAllCountries()
        {
            try
            {
                var result = await _countryService.GetAll();
                return Ok(new ResponseWrapper<IEnumerable<CountryWithIdRequest>>()
                {
                    Result = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseWrapper<object>
                {
                    Errors = new List<Error>()
                    {
                        new Error{ Message = ex.Message}
                    }
                });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetCountryById(int id)
        {
            try
            {
                var result = await _countryService.GetById(id);
                return Ok(new ResponseWrapper<CountryWithIdRequest>()
                {
                    Result = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseWrapper<object>
                {
                    Errors = new List<Error>()
                    {
                        new Error{ Message = ex.Message}
                    }
                });
            }
        }

        [HttpGet("{name}")]
        public async Task<ActionResult> GetCountryByName(string name)
        {
            try
            {
                var result = await _countryService.GetByName(name);
                return Ok(new ResponseWrapper<CountryWithIdRequest>()
                {
                    Result = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new ResponseWrapper<object>
                {
                    Errors = new List<Error>()
                    {
                        new Error{ Message = ex.Message}
                    }
                });
            }
        }

        [HttpPost()]
        public async Task<ActionResult> AddCountry([FromBody] NewCountryRequest newCountry)
        {
            try
            {
                var result = await _countryService.AddNewCountryAsync(newCountry);
                return Ok(new ResponseWrapper<object>()
                {
                    Result = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ResponseWrapper<object>
                {
                    Errors = new List<Error>()
                    {
                        new Error{ Message = ex.Message}
                    }
                });
            }
        }

        [HttpPut()]
        public async Task<ActionResult> UpdateCountry([FromBody] CountryWithIdRequest country)
        {
            try
            {
                var result = await _countryService.UpdateCountryAsync(country);
                return Ok(new ResponseWrapper<object>()
                {
                    Result = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new ResponseWrapper<object>
                {
                    Errors = new List<Error>()
                    {
                        new Error{ Message = ex.Message}
                    }
                });
            }
        }
    }
}
