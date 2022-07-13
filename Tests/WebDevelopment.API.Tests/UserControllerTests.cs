using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using WebDevelopment.API.Model;

namespace WebDevelopment.API.Tests;

public class UserControllerTests
{
    [Theory]
    [InlineData("")]
    [InlineData("1")]
    [InlineData("test@example.com")]
    public async Task GetRequests_ReturnSuccess(string url)
    {
        // Arrange
        var application = new WebApplicationFactory<Program>();
        var httpClient = application.CreateClient();
        httpClient.BaseAddress = new Uri($"https://localhost:7087/api/User/");

        //Act
        var response = await httpClient.GetAsync(url);

        //Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json; charset=utf-8",
            response?.Content?.Headers?.ContentType?.ToString());
    }

    [Fact]
    public async Task PostRequest_DoNotPassValidation_ReturnBadRequest()
    {
        // Arrange
        var application = new WebApplicationFactory<Program>();
        var httpClient = application.CreateClient();
        httpClient.BaseAddress = new Uri($"https://localhost:7087/api/User/");

        //Act
        var response = await httpClient.PostAsync("",
            new StringContent(JsonSerializer.Serialize(new NewUserRequest()), Encoding.UTF8, "application/json"));

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response?.StatusCode);
    }

    [Fact]
    public async Task PutRequest_DoNotPassValidation_ReturnBadRequest()
    {
        // Arrange
        var application = new WebApplicationFactory<Program>();
        var httpClient = application.CreateClient();
        httpClient.BaseAddress = new Uri($"https://localhost:7087/api/User/");

        //Act
        var response = await httpClient.PostAsync("",
            new StringContent(JsonSerializer.Serialize(new UpdateUserRequest()), Encoding.UTF8, "application/json"));

        //Assert
        Assert.Equal(HttpStatusCode.BadRequest, response?.StatusCode);
    }
}