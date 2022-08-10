
using WebDevelopment.Common.Requests.Country;

namespace WebDevelopment.Domain.Country.Services
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryWithIdRequest>> GetAll();

        Task<CountryWithIdRequest> GetById(int id);

        Task<CountryWithIdRequest> GetByName(string name);

        Task<bool> AddNewCountryAsync(NewCountryRequest countryRequest);

        Task<bool> UpdateCountryAsync(CountryWithIdRequest countryWithIdRequest);
    }
}
