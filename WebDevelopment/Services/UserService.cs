using Dapper;
using System.Data.SqlClient;
using WebDevelopment.API.Model;

namespace WebDevelopment.API.Services;

public class UserService : IUserService
{
    private readonly string _connectionString;

    public UserService(IConfiguration configuration)
    {
        _connectionString = configuration["ConnectionStrings:DefaultConnection"];
    }

    public IEnumerable<NewUserRequest> GetAllUsers()
    {
        List<NewUserRequest> users;
        using (var connection =
               new SqlConnection(_connectionString))
        {
            var sqlExpression = "SELECT * FROM Users";
            users = connection.Query<NewUserRequest>(sqlExpression).ToList();
        }

        return users;
    }

    public NewUserRequest GetUserById(int id)
    {
        NewUserRequest newUserRequest;
        using (var connection =
               new SqlConnection(_connectionString))
        {
            var sqlExpression = "SELECT * FROM Users WHERE Id = @Id";
            newUserRequest = connection.QueryFirstOrDefault<NewUserRequest>(sqlExpression, new { Id = id });
        }

        return newUserRequest;
    }

    public NewUserRequest GetUserByEmail(string userEmail)
    {
        NewUserRequest newUserRequest;
        using (var connection =
               new SqlConnection(_connectionString))
        {
            var sqlExpression = "SELECT * FROM Users WHERE UserEmail = @Email";
            newUserRequest = connection.QueryFirstOrDefault<NewUserRequest>(sqlExpression, new { Email = userEmail });
        }

        return newUserRequest;
    }

    public async Task CreateNewUserAsync(NewUserRequest userRequest)
    {
        await using (var connection =
                     new SqlConnection(_connectionString))
        {
            var sqlExpression = "INSERT INTO Users (FirstName, SecondName, UserEmail) VALUES (@Name, @Surname, @Email)";
            await connection.ExecuteAsync(sqlExpression,
                new { userRequest.Name, userRequest.Surname, userRequest.Email });
        }
    }

    public async Task UpdateNewUserAsync(UpdateUserRequest userRequest)
    {
        await using (var connection =
                     new SqlConnection(_connectionString))
        {
            var sqlExpression = "UPDATE Users SET FirstName = @Name, SecondName = @Surname, UserEmail = @Email WHERE Id = @Id";
            await connection.ExecuteAsync(sqlExpression,
                new { userRequest.Id, userRequest.Name, userRequest.Surname, userRequest.Email });
        }
    }
}