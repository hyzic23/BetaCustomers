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
            // Todo 
            // Uncomment user_id  when needed. - Using username for claims only  here
            //new Claim("user_id", user.Id.ToString()),
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

    public static bool ValidateJwtToken(string jwt)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(Secret);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };
            
            tokenHandler.ValidateToken(jwt, validationParameters, out SecurityToken validateToken);
            var validateJwt = (JwtSecurityToken)validateToken;
            
            // get claims 
            // Todo 
            // Uncomment userId  when needed. - Using username for claims only  here
            //var userId = validateJwt.Claims.First(claim => claim.Type == "user_id").Value;
            var userId = validateJwt.Claims.First(claim => claim.Type == "username").Value;
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}