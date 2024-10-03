using BetaCustomers.API.Config;
using BetaCustomers.API.IServices;
using BetaCustomers.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace BetaCustomers.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _userService;
    private readonly IMemoryCache _memoryCache;
    private readonly UsersApiConfig _usersApiConfig;
    private readonly ICacheService _cacheService;
    private readonly double _cacheExpiryTime;

    public UsersController(IUsersService userService, 
                           IMemoryCache memoryCache,
                           IOptions<UsersApiConfig> options,
                           ICacheService cacheService)
    {
        _userService = userService;
        _memoryCache = memoryCache;
        _usersApiConfig = options.Value;
        _cacheService = cacheService;
        _cacheExpiryTime = double.Parse(_usersApiConfig.CachingExpiryTimeInMinutes);
    }

    [HttpGet(Name = "users-test")]
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
    [Route("users")]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
    {
        //var cacheData = _memoryCache.Get<IEnumerable<UserModel>>("users");
        var cacheData = _cacheService.GetData<IEnumerable<UserModel>>("users");
        if (cacheData != null)
        {
            return Ok(cacheData);
        }

        //var cacheExpiryTime = double.Parse(_usersApiConfig.CachingExpiryTimeInMinutes);
        
         var expirationTime = DateTimeOffset.Now.AddMinutes(_cacheExpiryTime);
         cacheData = await _userService.GetUsers(cancellationToken);
         var userModels = cacheData.ToList();
         
         //Set cache for users
         //_memoryCache.Set("users", userModels, expirationTime);
         _cacheService.SetData("users", userModels, expirationTime);
        
        if (userModels.Any())
        {
            return Ok(cacheData);
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
        var cacheData = _cacheService.GetData<UserModel>("users");
        if (cacheData != null)
        {
            return Ok(cacheData);
        }
        
        var expirationTime = DateTimeOffset.Now.AddMinutes(_cacheExpiryTime);
        cacheData = await _userService.GetUserById(id);
        var userModels = cacheData;
         
        //Set cache for users
        _cacheService.SetData("users", userModels, expirationTime);
        
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
        _cacheService.RemoveData("users");
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
        _cacheService.RemoveData("users");
        return Ok(deleteUser);
    }

}
