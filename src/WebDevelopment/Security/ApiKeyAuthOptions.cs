using Microsoft.AspNetCore.Authentication;

namespace WebDevelopment.API.Security;

public class ApiKeyAuthOptions : AuthenticationSchemeOptions
{
    public const string DefaultScheme = "ApiKey";
    
    public string? ApiKey { get; set; }
}