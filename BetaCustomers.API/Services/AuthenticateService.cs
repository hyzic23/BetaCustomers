using BetaCustomers.API.IServices;
using BetaCustomers.API.Models;
using BetaCustomers.API.Utils;

namespace BetaCustomers.API.Services;

public class AuthenticateService : IAuthenticateService
{
    private IUsersService _usersService;
    private ILoginService _loginService;

    public AuthenticateService(IUsersService usersService, 
                               ILoginService loginService)
    {
        _usersService = usersService;
        _loginService = loginService;
    }

    public async Task<BaseResponse> AuthenticateUser(AuthenticateRequest request)
    {
        // Check if user exists
        var user = await _usersService.CheckIfUserExist(request.Username);
        if (user == null)
        {
            return new BaseResponse(false, "Invalid username or password");
        }
        
        // Generate JWT
        var jwt = JwtUtils.GenerateJwtToken(user);
        
        // Save token in login details
        var loginDetails = await _loginService.GetLoginDetails(user.Username);

        if (loginDetails == null)
        {
            loginDetails = new LoginDetail();
            loginDetails.UserId = user.Username;
            loginDetails.Token = jwt;
        }
        else
        {
            loginDetails.Token = jwt;
        }
        await _loginService.CreateLoginDetails(loginDetails);
        return new BaseResponse(true, jwt);
    }
}