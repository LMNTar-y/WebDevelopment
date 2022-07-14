using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using WebDevelopment.API.Model;
using WebDevelopment.API.Services;

namespace WebDevelopment.API.Tests;

public class UserControllerTests
{
    private readonly Mock<IUserService> _userServiceMock = new();
    private readonly WebApplicationFactory<Program> _factory = new();
    private readonly HttpClient _client;

    public UserControllerTests()
    {
        _userServiceMock.Setup(us => us.UpdateUserAsync(It.IsAny<UpdateUserRequest>()));
        _userServiceMock.Setup(us => us.CreateNewUserAsync(It.IsAny<NewUserRequest>()));
        _client = _factory.WithWebHostBuilder(
                builder => builder.ConfigureTestServices(
                    services => services.AddTransient(_ => _userServiceMock.Object)))
            .CreateClient();
        _client.BaseAddress = new Uri("https://localhost:7087/api/User/");
    }

    [Theory]
    [InlineData("")]
    [InlineData("1")]
    [InlineData("test@example.com")]
    public async Task GetRequests_ReturnSuccess(string url)
    {
        // Arrange
        var application = new WebApplicationFactory<Program>();
        var httpClient = application.CreateClient();
        httpClient.BaseAddress = new Uri("https://localhost:7087/api/User/");

        //Act
        var response = await httpClient.GetAsync(url);

        //Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json; charset=utf-8",
            response.Content.Headers.ContentType?.ToString());
    }

    [Fact]
    public async Task PostRequest_DoNotPassValidation_ReturnBadRequest()
    {
        // Arrange

        //Act
        var response = await _client.PostAsync("",
            new StringContent(JsonSerializer.Serialize(new NewUserRequest()), Encoding.UTF8, "application/json"));

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PostRequest_PassValidation_ReturnOk()
    {
        // Arrange
        var user = new NewUserRequest() { FirstName = "Test", SecondName = "Test", UserEmail = "test@test.test" };

        //Act
        var response = await _client.PostAsync("",
            new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json"));

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task PutRequest_DoNotPassValidation_ReturnBadRequest()
    {
        // Arrange
        //Act
        var response = await _client.PutAsync("",
            new StringContent(JsonSerializer.Serialize(new UpdateUserRequest()), Encoding.UTF8, "application/json"));

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PutRequest_PassValidation_ReturnOK()
    {
        // Arrange
        var user = new UpdateUserRequest()
            { Id = 1, FirstName = "Test", SecondName = "Test", UserEmail = "test@test.test" };

        //Act
        var response = await _client.PutAsync("",
            new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json"));

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}