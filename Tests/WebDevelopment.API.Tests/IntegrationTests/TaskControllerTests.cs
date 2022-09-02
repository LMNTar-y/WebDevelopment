using System.Net;
using System.Text;
using System.Text.Json;
using WebDevelopment.Common.Requests.Task;

namespace WebDevelopment.API.Tests.IntegrationTests;

[Collection("WebApplicationFactory collection")]
public class TaskControllerTests
{
    private readonly WebApplicationFactorySetupMock _setupMock;
    private readonly HttpClient _client;

    public TaskControllerTests(WebApplicationFactorySetupMock setupMock)
    {
        _setupMock = setupMock;
        _client = _setupMock.Setup();
        _client.BaseAddress = new Uri("https://localhost/api/Task/");
    }

    [Theory]
    [InlineData("")]
    [InlineData("1")]
    [InlineData("sometext")]
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
            new StringContent(JsonSerializer.Serialize(new NewTaskRequest(){Name = "q", Description = "s"}), Encoding.UTF8, "application/json"));

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PostRequest_PassValidation_ReturnCreated()
    {
        // Arrange
        var task = new NewTaskRequest() { Name = "Test1", Description = "Test description should contain more than 20 letters"};

        //Act
        var response = await _client.PostAsync("",
            new StringContent(JsonSerializer.Serialize(task), Encoding.UTF8, "application/json"));

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
            new StringContent(JsonSerializer.Serialize(new TaskWithIdRequest()), Encoding.UTF8, "application/json"));

        //Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Content);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PutRequest_PassValidation_ReturnOK()
    {
        // Arrange
        var task = new TaskWithIdRequest()
        { Id = 1, Name = "Test", Description = "Test description should contain more than 20 letters" };

        //Act
        var response = await _client.PutAsync("",
            new StringContent(JsonSerializer.Serialize(task), Encoding.UTF8, "application/json"));

        //Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Content);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

}