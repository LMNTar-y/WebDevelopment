using WebDevelopment.Common.Requests.Country;
using WebDevelopment.Domain.IRepos;
using WebDevelopment.Infrastructure.Models;

namespace WebDevelopment.Infrastructure.Repos;

public class CountryRepo : GenericRepository<Country>, ICountryRepo
{
    public CountryRepo(WebDevelopmentContext context) : base(context)
    {
    }

    public new async Task<IEnumerable<ICountryRequest>> GetAllAsync(string includeProperties = "")
    {
        var result = await base.GetAllAsync(includeProperties);
        return result.Select(Map);
    }

    public new async Task<ICountryRequest> GetByIdAsync(object id)
    {
        if (id is < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "Id in the request should be more than 0");
        }

        var result = await base.GetByIdAsync(id);
        return Map(result);
    }

    public async Task<ICountryRequest> GetByNameAsync(string name)
    {
        var result = (await base.GetAllAsync()).FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)) ??
                     throw new ArgumentNullException(nameof(name), $"Country with name: \"{name}\" was not found in the DataBase");

        return Map(result);
    }

    public async Task<ICountryRequest> AddAsync(ICountryRequest entity)
    {
        var result = Map((NewCountryRequest)entity);
        await AddAsync(result);
        return entity;
    }

    public async Task<ICountryRequest> UpdateAsync(ICountryRequest entity)
    {
        var result = Map((CountryWithIdRequest)entity);
        await UpdateAsync(result);
        return entity;
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