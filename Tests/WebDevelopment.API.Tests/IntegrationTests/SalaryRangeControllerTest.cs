using System.Net;
using System.Text;
using System.Text.Json;
using WebDevelopment.Common.Requests.SalaryRange;

namespace WebDevelopment.API.Tests.IntegrationTests;

[Collection("WebApplicationFactory collection")]
public class SalaryRangeControllerTest
{
    private readonly WebApplicationFactorySetupMock _setupMock;
    private readonly HttpClient _client;

    public SalaryRangeControllerTest(WebApplicationFactorySetupMock setupMock)
    {
        _setupMock = setupMock;
        _client = _setupMock.Setup();
        _client.BaseAddress = new Uri("https://localhost/api/SalaryRange/");
    }

    [Theory]
    [InlineData("")]
    [InlineData("1")]
    [InlineData("Sometext")]

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
        var pos = new NewSalaryRangeRequest() { StartRange = 1000, FinishRange = 500};

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
        var pos = new NewSalaryRangeRequest() { StartRange = 500, FinishRange = 1000, CountryName = "somename", PositionName = "somename"};

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
            new StringContent(JsonSerializer.Serialize(new SalaryRangeWithIdRequest()), Encoding.UTF8, "application/json"));

        //Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Content);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PutRequest_PassValidation_ReturnOK()
    {
        // Arrange
        var dep = new SalaryRangeWithIdRequest()
        { Id = 1, StartRange = 500, FinishRange = 1000, CountryName = "somename", PositionName = "somename"};

        //Act
        var response = await _client.PutAsync("",
            new StringContent(JsonSerializer.Serialize(dep), Encoding.UTF8, "application/json"));

        //Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Content);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}