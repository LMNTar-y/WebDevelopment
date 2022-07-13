using javax.xml.ws;
using Microsoft.AspNetCore.Mvc;
using WebDevelopment.API.Model;
using WebDevelopment.API.Services;

namespace WebDevelopment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet()]
    public IEnumerable<NewUserRequest> GetAllUsers()
    {
        return _userService.GetAllUsers();
    }

    [HttpGet("{id:int}")]
    public NewUserRequest GetUserById(int id)
    {
        return _userService.GetUserById(id);
    }

    [HttpGet("{userEmail}")]
    public NewUserRequest GetUserByEmail(string userEmail)
    {
        return _userService.GetUserByEmail(userEmail);
    }

    [HttpPost()]
    public async Task<ActionResult> SaveAsync([FromBody] NewUserRequest userRequest)
    {
        await _userService.CreateNewUserAsync(userRequest);

        return Ok();
    }

    [HttpPut()]
    public async Task<ActionResult> UpdateAsync([FromBody] UpdateUserRequest userRequest)
    {
        await _userService.UpdateNewUserAsync(userRequest);

        return Ok();
    }
}