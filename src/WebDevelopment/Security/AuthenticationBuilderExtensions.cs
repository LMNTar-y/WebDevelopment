using Microsoft.AspNetCore.Authentication;

namespace WebDevelopment.API.Security
{
    public static class AuthenticationBuilderExtensions
    {
        public static AuthenticationBuilder AddApiKeyAuth(this AuthenticationBuilder builder, string authenticationScheme, Action<ApiKeyAuthOptions> configureOptions) 
        {
            return builder.AddScheme<ApiKeyAuthOptions, ApiKeyAuthHandler>(authenticationScheme, configureOptions);
        }
    }
}
