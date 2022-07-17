using System.Net;
using System.Text;
using System.Text.Json;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using WebDevelopment.API.Model;
using WebDevelopment.API.Services;

namespace WebDevelopment.API.Tests.IntegrationTests;

public class UserControllerTests
{
    private readonly Mock<IUserService> _userServiceMock = new();
    private readonly WebApplicationFactory<Program> _factory = new();
    private readonly HttpClient _client;

    public UserControllerTests()
    {
        _userServiceMock.Setup(us => us.UpdateUserAsync(It.IsAny<UpdateUserRequest>()));
        _userServiceMock.Setup(us => us.CreateNewUserAsync(It.IsAny<NewUserRequest>()));
        _userServiceMock.Setup(us => us.GetUserById(It.Is<int>(i => i > 0))).Returns(() => new NewUserRequest());
        _userServiceMock.Setup(us => us.GetUserByEmail(It.IsAny<string>())).Returns(() => new NewUserRequest());
        _userServiceMock.Setup(us => us.GetAllUsers()).Returns(() => new List<NewUserRequest>());
        _client = _factory.WithWebHostBuilder(
                builder => builder.ConfigureTestServices(
                    services =>
                    {
                        services.AddTransient(_ => _userServiceMock.Object);
                        services.AddAuthentication("Test")
                            .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", _ => { });
                    }))
            .CreateClient();
        _client.BaseAddress = new Uri("https://localhost/api/User/");
    }

    [Theory]
    [InlineData("")]
    [InlineData("1")]
    [InlineData("test@example.com")]
    public async Task GetRequests_ReturnSuccess(string url)
    {

        //Act
        var response = await _client.GetAsync(url);

        //Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Content);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
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
        Assert.NotNull(response);
        Assert.NotNull(response.Content);
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
        Assert.NotNull(response);
        Assert.NotNull(response.Content);
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
        Assert.NotNull(response);
        Assert.NotNull(response.Content);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}