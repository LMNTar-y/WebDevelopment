using System.Net;
using System.Text;
using System.Text.Json;
using WebDevelopment.Common.Requests.User;

namespace WebDevelopment.API.Tests.IntegrationTests;

[Collection("WebApplicationFactory collection")]
public class UserControllerTests
{
    private readonly WebApplicationFactorySetupMock _setupMock;
    private readonly HttpClient _client;

    public UserControllerTests(WebApplicationFactorySetupMock setupMock)
    {
        _setupMock = setupMock;
        _client = _setupMock.Setup();
        _client.BaseAddress = new Uri("https://localhost/api/User/");
    }

    [Theory]
    [InlineData("")]
    [InlineData("1")]
    [InlineData("test@example.com")]
    public async Task GetRequests_ReturnSuccess(string url)
    {
        //Arrange 
        //Act
        var response = await _client.GetAsync(url);

        //Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Content);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetByIdRequest_404NotFound_WhenIdLessThanOne()
    {
        //Arrange 
        var id = "-1";
        //Act
        var response = await _client.GetAsync(id);

        //Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Content);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
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
    public async Task PostRequest_PassValidation_ReturnCreated()
    {
        // Arrange
        var user = new NewUserRequest() { FirstName = "Test2", SecondName = "Test2", UserEmail = "test@test.test2" };

        //Act
        var response = await _client.PostAsync("",
            new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json"));

        //Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Content);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task PutRequest_DoNotPassValidation_ReturnBadRequest()
    {
        // Arrange
        //Act
        var response = await _client.PutAsync("",
            new StringContent(JsonSerializer.Serialize(new UserWithIdRequest()), Encoding.UTF8, "application/json"));

        //Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Content);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PutRequest_PassValidation_ReturnOK()
    {
        // Arrange
        var user = new UserWithIdRequest()
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