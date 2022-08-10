
using WebDevelopment.Common.Requests.Country;

namespace WebDevelopment.Domain.Country.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository ?? throw new ArgumentException($"{nameof(countryRepository)} was not downloaded from DI"); 
        }

        public async Task<IEnumerable<CountryWithIdRequest>> GetAll()
        {
            var result = await _countryRepository.GetAll();
            return result;
        }

        public async Task<CountryWithIdRequest> GetById(int id)
        {
            var result = (await _countryRepository.GetAll()).FirstOrDefault(x => x.Id == id) ??
                         throw new ArgumentNullException(nameof(id), $"Country with id:\"{id}\" has not fount in the DataBase");

            return result;
        }

        public async Task<CountryWithIdRequest> GetByName(string name)
        {
            var result = (await _countryRepository.GetAll()).FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)) ??
                         throw new ArgumentNullException(nameof(name), $"Country with name: \"{name}\" has not fount in the DataBase");

            return result;
        }

        public async Task<bool> AddNewCountryAsync(NewCountryRequest countryRequest)
        {
            await _countryRepository.Add(countryRequest);
            return true;
        }

        public async Task<bool> UpdateCountryAsync(CountryWithIdRequest countryWithIdRequest)
        {
            await _countryRepository.Update(countryWithIdRequest);
            return true;
        }
    }
}
