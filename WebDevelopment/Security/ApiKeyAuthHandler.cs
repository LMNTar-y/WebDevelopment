using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace WebDevelopment.API.Security
{
	public static class ApiKeyAuth
	{
		public const string AuthenticationType = "ApiKey auth type";
	}

	public class ApiKeyAuthHandler : AuthenticationHandler<ApiKeyAuthOptions>
	{
        public ApiKeyAuthHandler(IOptionsMonitor<ApiKeyAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
			: base(options, logger, encoder, clock)
		{
        }

		protected override Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			// Get Authorization header value
			if (!this.Request.Headers.TryGetValue(HeaderNames.Authorization, out var authorization))
			{
				return Task.FromResult(AuthenticateResult.Fail($"Cannot read authorization header."));
			}

			// The auth key from Authorization header check against the configured one
			if (authorization != this.Options.ApiKey)
			{
				return Task.FromResult(AuthenticateResult.Fail("Invalid auth key."));
			}

			// Create authenticated user
			var claimsIdentity =
				new ClaimsIdentity(new List<Claim>()
					{
						new Claim(ClaimTypes.Name, "Superuser"),
                        new Claim("AppName", "WebApplication")
					},
					ApiKeyAuth.AuthenticationType);
			var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), "ApiKey");

            return Task.FromResult(AuthenticateResult.Success(ticket));
		}
	}
}
