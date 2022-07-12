using System.Data.SqlClient;
using Dapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WebDevelopment.API.Middleware;
using WebDevelopment.API.Model;
using WebDevelopment.API.Model.Validators;

namespace WebDevelopment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IValidator<User> _validator;
    private readonly string _connectionString;

    public UserController(IConfiguration configuration, IValidator<User> validator)
    {
        _connectionString = configuration["ConnectionStrings:DefaultConnection"];
        _validator = validator;
    }

    [HttpGet()]
    public IEnumerable<User> GetAllUsers()
    {
        List<User> users;
        using (var connection =
               new SqlConnection(_connectionString))
        {
            var sqlExpression = "SELECT * FROM Users";
            users = connection.Query<User>(sqlExpression).ToList();
        }

        return users;
    }

    [HttpGet("{id:int}")]
    public User GetUserById(int id)
    {
        User user;
        using (var connection =
               new SqlConnection(_connectionString))
        {
            var sqlExpression = "SELECT * FROM Users WHERE Id = @Id";
            user = connection.QueryFirstOrDefault<User>(sqlExpression, new { Id = id });
        }

        return user;
    }

    [HttpGet("{userEmail}")]
    public User GetUserByEmail(string userEmail)
    {
        User user;
        using (var connection =
               new SqlConnection(_connectionString))
        {
            var sqlExpression = "SELECT * FROM Users WHERE Email = @Email";
            user = connection.QueryFirstOrDefault<User>(sqlExpression, new { Email = userEmail });
        }

        return user;
    }

    [HttpPost()]
    public async Task<ActionResult> CreateNewUserAsync([FromBody] User newUser)
    {
        ValidationResult result = await _validator.ValidateAsync(newUser);

        if (!result.IsValid)
        {
            result.AddToModelState(this.ModelState);
            throw new AppException("newUser object did not pass validation");
        }

        await using (var connection =
               new SqlConnection(_connectionString))
        {
            var sqlExpression = "INSERT INTO Users (Name, Surname, Email) VALUES (@Name, @Surname, @Email)";
            await connection.ExecuteAsync(sqlExpression, new { newUser.Name, newUser.Surname, newUser.Email });
        }

        return Ok();
    }
}