using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BetaCustomers.API.Config;
using BetaCustomers.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BetaCustomers.API.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly string _secretKey;
    private readonly IOptions<UsersApiOptions> _options;
    private readonly ILogger<LoginController> _logger;
    public LoginController(IOptions<UsersApiOptions> options, 
                           ILogger<LoginController> logger)
    {
        _options = options;
        _secretKey = options.Value.SecretKey;
        _logger = logger;
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("loggers")]
    public IActionResult Get()
    {
        _logger.LogInformation("User found successfully!");
        _logger.LogWarning("User was not found in good condition!");
        _logger.LogCritical("User is very critical");
        _logger.LogError("I am sorry but the user is already dead!");
        return Ok("Logs information successfully!");
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("token")]
    public async Task<IActionResult> GenerateToken(UserModel userModel)
    {
        if (userModel == null)
        {
            return BadRequest();
        }
        var user = AuthenticateUser(userModel);
        var token = await GenerateJwtTokens(user);
        return Ok(token);
    }

    private Task<string> GenerateJwtTokens(UserModel userInfo)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userInfo.Id.ToString()),
            new Claim(ClaimTypes.Name, userInfo.Username),
        };

        //TODO:
        //Make AddDays(30) read from AppSettings configuration
        var jwtToken = new JwtSecurityToken(
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddDays(30),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(

                    Encoding.UTF8.GetBytes(_secretKey)),
                SecurityAlgorithms.HmacSha256Signature
            )
        );
        return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(jwtToken));
    }


    //Method to authenticate user
    private static UserModel AuthenticateUser(UserModel login)
    {
        //TODO
        //Implement username, email to be dynamic
        //by reading from database and removing the hardcoded values.
        
        //Validate  User Credential
        //Demo Purpose with HardCoded values
        if (login.Username == "sys")
        {
            var user = new UserModel
            {
                Id = login.Id,
                Username = "sys",
                Email = "sys.admin@imodetechnologies.com"
            };
            return user;
        }
        return null!;
    }
}