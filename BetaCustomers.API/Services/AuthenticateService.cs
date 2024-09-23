using BetaCustomers.API.IServices;
using BetaCustomers.API.Models;
using BetaCustomers.API.Utils;

namespace BetaCustomers.API.Services;

public class AuthenticateService : IAuthenticateService
{
    private readonly IUsersService _usersService;
    private readonly ILoginService _loginService;

    public AuthenticateService(IUsersService usersService, 
                               ILoginService loginService)
    {
        _usersService = usersService;
        _loginService = loginService;
    }

    public async Task<BaseResponse> AuthenticateUser(AuthenticateRequest request)
    {
        // Todo :
        // Change this to check for username and password
        var user = await _usersService.CheckIfUserExist(request.Username);
        if (user == null)
        {
            return new BaseResponse(StatusCodes.Status401Unauthorized, new MessageDTO("Invalid username or password"));
        }
        
        // Generate JWT
        var jwt = JwtUtils.GenerateJwtToken(user);
        
        // Save token in login details
        var loginDetails = await _loginService.GetLoginDetails(user.Username);

        if (string.IsNullOrEmpty(loginDetails.Username))
        {
            loginDetails = new LoginDetail
            {
                Username = user.Username,
                Token = jwt,
                CreatedAt = DateTime.UtcNow
            };
        }
        else
        {
            loginDetails.Token = jwt;
        }
        await _loginService.CreateLoginDetails(loginDetails);
        return new BaseResponse(StatusCodes.Status201Created, new MessageDTO(jwt));
    }
}