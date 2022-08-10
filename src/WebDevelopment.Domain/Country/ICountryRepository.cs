using WebDevelopment.Common.Requests.Country;

namespace WebDevelopment.Domain.Country
{
    public interface ICountryRepository : IDefaultRepository<CountryWithIdRequest, NewCountryRequest>
    {
    }
}
