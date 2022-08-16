﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebDevelopment.Common.Requests.Country;
using WebDevelopment.Domain;

namespace WebDevelopment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CountryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CountryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet()]
        public async Task<ActionResult> GetAllCountries()
        {
            try
            {
                var result = await _unitOfWork.CountryRepo.GetAllAsync();
                return Ok(new ResponseWrapper<IEnumerable<ICountryRequest>>()
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
                var result = await _unitOfWork.CountryRepo.GetByIdAsync(id);
                return Ok(new ResponseWrapper<ICountryRequest>()
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
                var result = await _unitOfWork.CountryRepo.GetByNameAsync(name);
                return Ok(new ResponseWrapper<ICountryRequest>()
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
                var result = await _unitOfWork.CountryRepo.AddAsync(newCountry);
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
                var result = await _unitOfWork.CountryRepo.UpdateAsync(country);
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
