using System.Net;
using System.Text;
using System.Text.Json;
using WebDevelopment.Common.Requests.Task;
using WebDevelopment.Common.Requests.User;
using WebDevelopment.Common.Requests.UserTask;


namespace WebDevelopment.API.Tests.IntegrationTests;

[Collection("WebApplicationFactory collection")]
public class UserTaskControllerTest
{
    private readonly WebApplicationFactorySetupMock _setupMock;
    private readonly HttpClient _client;

    public UserTaskControllerTest(WebApplicationFactorySetupMock setupMock)
    {
        _setupMock = setupMock;
        _client = _setupMock.Setup();
        _client.BaseAddress = new Uri("https://localhost/api/UserTask/");
    }

    [Theory]
    [InlineData("")]
    [InlineData("1")]
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
        var userTAsk = new NewUserTaskRequest();

        //Act
        var response = await _client.PostAsync("",
            new StringContent(JsonSerializer.Serialize(userTAsk), Encoding.UTF8, "application/json"));

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PostRequest_PassValidation_ReturnOk()
    {
        // Arrange
        var userTask = new NewUserTaskRequest()
        {
            User = new NewUserRequest(), Task = new NewTaskRequest(), StartDate = DateTimeOffset.Now,
            FinishDate = DateTimeOffset.Now.AddDays(1), ValidTill = DateTimeOffset.Now.AddDays(1)
        };

        //Act
        var response = await _client.PostAsync("",
            new StringContent(JsonSerializer.Serialize(userTask), Encoding.UTF8, "application/json"));

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
            new StringContent(JsonSerializer.Serialize(new UserTaskWithIdRequest()), Encoding.UTF8,
                "application/json"));

        //Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Content);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PutRequest_PassValidation_ReturnOK()
    {
        // Arrange
        var userTask = new UserTaskWithIdRequest()
        {
            Id = 1,
            User = new NewUserRequest(),
            Task = new NewTaskRequest(),
            StartDate = DateTimeOffset.Now,
            FinishDate = DateTimeOffset.Now.AddDays(1),
            ValidTill = DateTimeOffset.Now.AddDays(1)
        };

        //Act
        var response = await _client.PutAsync("",
            new StringContent(JsonSerializer.Serialize(userTask), Encoding.UTF8, "application/json"));

        //Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Content);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}