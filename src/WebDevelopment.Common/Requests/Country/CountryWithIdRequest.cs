namespace WebDevelopment.Common.Requests.Country;

public class CountryWithIdRequest : ICountryRequest
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Alpha3Code { get; set; }
}