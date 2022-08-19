using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using Quartz.Core;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Logging;
using WebDevelopment.API.Tests.Mocks;
using WebDevelopment.Domain;

namespace WebDevelopment.API.Tests;

public class WebApplicationFactorySetupMock : IDisposable
{
    private readonly WebApplicationFactory<Program> _factory = new();
    private readonly UnitOfWorkMock _unitOfWork = new();
    private HttpClient? _client;
    public HttpClient Setup()
    {
        _unitOfWork.Setup_UserRepo();
        _client = _factory.WithWebHostBuilder(
                builder => builder.ConfigureTestServices(
                    services =>
                    {
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType ==
                                 typeof(IUnitOfWork)); 

                        if (descriptor != null)
                        {
                            services.Remove(descriptor);
                        }

                        services.AddTransient(_ => _unitOfWork.Object);
                        services.AddAuthentication("Test")
                            .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", _ => { });
                    }))
            .CreateClient();
        var token = Generate();
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

        return _client;
    }

    private string Generate()
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TwlLasrvrJ3bPXUk1BtP"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


        var token = new JwtSecurityToken("https://localhost:44328/",
            "https://localhost:44328/",
            null,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public void Dispose()
    {
        _factory.Dispose();
        _client?.Dispose();
    }
}

[CollectionDefinition("WebApplicationFactory collection")]
public class DatabaseCollection : ICollectionFixture<WebApplicationFactorySetupMock>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}