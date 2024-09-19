using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BetaCustomers.API.Models;
using Microsoft.IdentityModel.Tokens;

namespace BetaCustomers.API.Utils;

public class JwtUtils
{
    private const string Secret = "3hO4Lash4CzZfk0Ga6yQhd48208RGTAu";

    public static string GenerateJwtToken(UserModel user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(Secret);
        
        // token claims
        var claims = new List<Claim>
        {
            new Claim("user_id", user.Id.ToString()),
            new Claim("username", user.Username)
        };

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature)
        };

        var jwtToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(jwtToken);
    }
}