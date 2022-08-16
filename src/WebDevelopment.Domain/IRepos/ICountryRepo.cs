using WebDevelopment.Common.Requests.Country;

namespace WebDevelopment.Domain.IRepos;

public interface ICountryRepo : IGenericRepository<ICountryRequest>
{
    Task<ICountryRequest> GetByNameAsync(string name);
}