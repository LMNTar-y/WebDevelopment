using System.Net;
using System.Text;
using System.Text.Json;
using WebDevelopment.Common.Requests.Position;

namespace WebDevelopment.API.Tests.IntegrationTests;

[Collection("WebApplicationFactory collection")]
public class PositionControllerTest
{
    private readonly WebApplicationFactorySetupMock _setupMock;
    private readonly HttpClient _client;

    public PositionControllerTest(WebApplicationFactorySetupMock setupMock)
    {
        _setupMock = setupMock;
        _client = _setupMock.Setup();
        _client.BaseAddress = new Uri("https://localhost/api/Position/");
    }

    [Theory]
    [InlineData("")]
    [InlineData("1")]
    [InlineData("SomePos")]

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
        var pos = new NewPositionRequest() { Name = "5", ShortName = "5" };

        //Act
        var response = await _client.PostAsync("",
            new StringContent(JsonSerializer.Serialize(pos), Encoding.UTF8, "application/json"));

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PostRequest_PassValidation_ReturnCreated()
    {
        // Arrange
        var pos = new NewPositionRequest() { Name = "Test2", ShortName = "test2"};

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
        //Act
        var response = await _client.PutAsync("",
            new StringContent(JsonSerializer.Serialize(new PositionWithIdRequest()), Encoding.UTF8, "application/json"));

        //Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Content);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PutRequest_PassValidation_ReturnOK()
    {
        // Arrange
        var dep = new PositionWithIdRequest()
        { Id = 1, Name = "Test5", ShortName = "22"};

        //Act
        var response = await _client.PutAsync("",
            new StringContent(JsonSerializer.Serialize(dep), Encoding.UTF8, "application/json"));

        //Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Content);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}