
using Microsoft.EntityFrameworkCore;
using WebDevelopment.Common.Requests.Country;
using WebDevelopment.Domain.Country;
using WebDevelopment.Infrastructure.Models;

namespace WebDevelopment.Infrastructure.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly WebDevelopmentContext _context;

        public CountryRepository(WebDevelopmentContext context)
        {
            _context = context ?? throw new ArgumentException($"{nameof(context)} was not downloaded from DI");
        }

        public async Task<IEnumerable<CountryWithIdRequest>> GetAll()
        {
            var result = await _context.Countries.ToListAsync();
            return result.Select(Map);
        }

        public async Task<NewCountryRequest> Add(NewCountryRequest country)
        {
            var result = Map(country);
            _context.Countries.Add(result);
            await _context.SaveChangesAsync();
            return country;
        }

        public async Task<CountryWithIdRequest> Update(CountryWithIdRequest countryWithId)
        {
            var result = Map(countryWithId);
            _context.Countries.Update(result);
            await _context.SaveChangesAsync();
            return countryWithId;
        }

        #region Mappers
        private static Country Map(NewCountryRequest countryRequest)
        {
            return new Country
            {
                Name = countryRequest.Name,
                Alpha3Code = countryRequest.Alpha3Code?.ToUpper()
            };
        }

        private static Country Map(CountryWithIdRequest countryWithId)
        {
            return new Country
            {
                Id = countryWithId.Id,
                Name = countryWithId.Name,
                Alpha3Code = countryWithId.Alpha3Code?.ToUpper()
            };
        }

        private static CountryWithIdRequest Map(Country country)
        {
            return new CountryWithIdRequest
            {
                Id = country.Id,
                Name = country.Name,
                Alpha3Code = country.Alpha3Code
            };
        }

        #endregion
    }
}
