using BetaCustomers.API.IServices;
using BetaCustomers.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace BetaCustomers.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    //private readonly ILogger<UsersController> _logger;
    private readonly IUsersService _userService;

    public UsersController(IUsersService userService)
    {
        _userService = userService;
    }

    [HttpGet(Name = "users")]
    public async Task<IActionResult> Get()
    {
        var users = await _userService.GetAllUsers();
        if (users.Any())
        {
            return Ok(users);
        }
        return NotFound();
    }
    
    [HttpGet]
    [Route("/")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetUsers();
        if (users.Any())
        {
            return Ok(users);
        }
        return NotFound();
    }
    
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateUser(UserModel user)
    {
        // Todo
        // Add user validations
        var users = await _userService.CreateUser(user);
        return Ok(users);
    }
    
    [HttpGet]
    [Route("get-user/{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var user = await _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> Update(UserModel user)
    {
        var userFound = await _userService.GetUserById(user.Id);
        if (userFound == null)
        {
            return NotFound();
        }
        var updatedUser = _userService.UpdateUser(userFound.Id, userFound);
        return Ok(updatedUser);
    }
    
    [HttpDelete]
    [Route("delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var userFound = await _userService.GetUserById(id);
        if (userFound == null)
        {
            return NotFound();
        }
        var deleteUser = _userService.DeleteUser(userFound.Id);
        return Ok(deleteUser);
    }

}
