using System.Net;
using System.Text;
using System.Text.Json;
using WebDevelopment.Common.Requests.Department;
using WebDevelopment.Common.Requests.Position;
using WebDevelopment.Common.Requests.User;
using WebDevelopment.Common.Requests.UserPosition;

namespace WebDevelopment.API.Tests.IntegrationTests;

[Collection("WebApplicationFactory collection")]
public class UserPositionControllerTests
{
    private readonly WebApplicationFactorySetupMock _setupMock;
    private readonly HttpClient _client;

    public UserPositionControllerTests(WebApplicationFactorySetupMock setupMock)
    {
        _setupMock = setupMock;
        _client = _setupMock.Setup();
        _client.BaseAddress = new Uri("https://localhost/api/UserPosition/");
    }

    [Theory]
    [InlineData("")]
    [InlineData("1")]
    [InlineData("sometext/sometext")]
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
    public async Task PostRequest_DoNotPassValidation_ReturnBadRequest()
    {
        // Arrange

        //Act
        var response = await _client.PostAsync("",
            new StringContent(JsonSerializer.Serialize(new NewUserPositionRequest()), Encoding.UTF8, "application/json"));

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PostRequest_PassValidation_ReturnCreated()
    {
        // Arrange
        var pos = new NewUserPositionRequest() { User = new UserWithIdRequest(), Position = new PositionWithIdRequest(), Department = new NewDepartmentRequest()};

        //Act
        var response = await _client.PostAsync("",
            new StringContent(JsonSerializer.Serialize(pos), Encoding.UTF8, "application/json"));

        //Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Content);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task PutRequest_DoNotPassValidation_ReturnBadRequest()
    {
        // Arrange
        var pos = new NewUserPositionRequest();
        //Act
        var response = await _client.PutAsync("",
            new StringContent(JsonSerializer.Serialize(pos), Encoding.UTF8, "application/json"));

        //Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Content);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PutRequest_PassValidation_ReturnOK()
    {
        // Arrange
        var pos = new UserPositionWithIdRequest()
        { Id = 1, User = new UserWithIdRequest(), Position = new PositionWithIdRequest(), Department = new NewDepartmentRequest() };

        //Act
        var response = await _client.PutAsync("",
            new StringContent(JsonSerializer.Serialize(pos), Encoding.UTF8, "application/json"));

        //Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Content);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

}