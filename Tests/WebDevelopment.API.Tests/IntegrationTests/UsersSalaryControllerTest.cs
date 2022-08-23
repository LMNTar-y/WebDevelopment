using System.Net;
using System.Text;
using System.Text.Json;
using WebDevelopment.Common.Requests.User;
using WebDevelopment.Common.Requests.UserSalary;


namespace WebDevelopment.API.Tests.IntegrationTests;

[Collection("WebApplicationFactory collection")]
public class UsersSalaryControllerTest
{
    private readonly WebApplicationFactorySetupMock _setupMock;
    private readonly HttpClient _client;

    public UsersSalaryControllerTest(WebApplicationFactorySetupMock setupMock)
    {
        _setupMock = setupMock;
        _client = _setupMock.Setup();
        _client.BaseAddress = new Uri("https://localhost/api/UsersSalary/");
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
        var userSalary = new NewUserSalaryRequest() ;

        //Act
        var response = await _client.PostAsync("",
            new StringContent(JsonSerializer.Serialize(userSalary), Encoding.UTF8, "application/json"));

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PostRequest_PassValidation_ReturnOk()
    {
        // Arrange
        var userSalary = new NewUserSalaryRequest() { Salary = 1000, User = new NewUserRequest() };

        //Act
        var response = await _client.PostAsync("",
            new StringContent(JsonSerializer.Serialize(userSalary), Encoding.UTF8, "application/json"));

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
            new StringContent(JsonSerializer.Serialize(new UserSalaryWithIdRequest()), Encoding.UTF8, "application/json"));

        //Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Content);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PutRequest_PassValidation_ReturnOK()
    {
        // Arrange
        var userSalary= new UserSalaryWithIdRequest()
        { Id = 1, Salary = 1000, User = new NewUserRequest() };

        //Act
        var response = await _client.PutAsync("",
            new StringContent(JsonSerializer.Serialize(userSalary), Encoding.UTF8, "application/json"));

        //Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Content);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}