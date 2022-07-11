using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace WebDevelopment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly string _connectionString;

    public UserController(IConfiguration configuration)
    {
        _connectionString = configuration["ConnectionStrings:DefaultConnection"];
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
    public ActionResult CreateNewUser([FromBody] User user)
    {
        using (var connection =
               new SqlConnection(_connectionString))
        {
            var sqlExpression = "INSERT INTO Users (Name, Surname, Email) VALUES (@Name, @Surname, @Email)";
            connection.Execute(sqlExpression, new { Name = user.Name, Surname = user.Surname, Email = user.Email });
        }

        return Ok();
    }
}