namespace WebDevelopment.Common.Requests.Country;

public class NewCountryRequest : ICountryRequest
{
    public string? Name { get; set; }

    public string? Alpha3Code { get; set; }
}