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
    private readonly IConfiguration _configuration;
    private readonly IOptions<UsersApiOptions> _options;
 
    public LoginController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [AllowAnonymous]
    [HttpPost(Name = "Login")]
    public async Task<IActionResult> Login(UserModel login)
    {
        var user = AuthenticateUser(login);
        var token = await GenerateJWTTokens(user);
        return Ok(token);
    }

    private async Task<string> GenerateJWTTokens(UserModel userInfo)
    {
        //UsersApiOptions options = new UsersApiOptions();
        //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes())
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userInfo.Id.ToString()),
            new Claim(ClaimTypes.Name, userInfo.Username),
        };

        var jwtToken = new JwtSecurityToken(
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddDays(30),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(

                    Encoding.UTF8.GetBytes("")),
                SecurityAlgorithms.HmacSha256Signature
            )
        );
        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }


    //Method to  authenticate user
    private UserModel AuthenticateUser(UserModel login)
    {
        UserModel user = null;
        
        //Validate  User Credential
        //Demo Purpose with HardCoded values
        if (user.Username == "sys")
        {
            user = new UserModel
            {
                Username = "Sys Admin",
                Email = "sys.admin@imodetechnologies.com"
            };
        }
        return user;
    }
}