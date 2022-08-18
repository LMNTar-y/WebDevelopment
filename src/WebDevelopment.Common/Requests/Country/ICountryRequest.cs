
namespace WebDevelopment.Common.Requests.Country
{
    public interface ICountryRequest
    {
        public string? Name { get; set; }

        public string? Alpha3Code { get; set; }
    }
}
