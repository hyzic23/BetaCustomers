using BetaCustomers.API.IServices;
using BetaCustomers.API.Models;
using BetaCustomers.API.Utils;

namespace BetaCustomers.API.Services;

public class AuthService : IAuthService
{
    private readonly IUsersService _usersService;
    private readonly ILoginService _loginService;
    private readonly ILogger<AuthService> _logger;

    public AuthService(IUsersService usersService, 
                       ILoginService loginService, 
                       ILogger<AuthService> logger)
    {
        _usersService = usersService;
        _loginService = loginService;
        _logger = logger;
    }

    ///  <summary>
    ///  Method is used to Authenticate request 
    ///  </summary>
    ///  <param name="request"></param>
    ///  <param name="cancellationToken"></param>
    ///  <returns>BaseResponse</returns>
    public async Task<BaseResponse> AuthenticateUser(AuthenticateRequest request, CancellationToken cancellationToken = default)
    {
        LoginDetail loginDetails = null;
        try
        {
            if (cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation("CancellationToken was initiated and Authenticate User Task Cancelled!");
                cancellationToken.ThrowIfCancellationRequested();
            }
            else
            {
                var user = await _usersService.ValidateUserCredential(request.Username, request.Password);
                if (user == null || string.IsNullOrEmpty(user.Username))
                {
                    _logger.LogInformation($"Username { request.Username } or Password is incorrect!");
                    var error = new { Error = "Unauthorized", Reason = "Invalid username or password" };
                    return new BaseResponse(StatusCodes.Status401Unauthorized, new MessageDTO(error));
                }

                // Generate JWT
                var jwt = JwtUtils.GenerateJwtToken(user);

                // Save token in login details
                loginDetails = await _loginService.GetLoginDetails(user.Username);

                if (string.IsNullOrEmpty(loginDetails.Username))
                {
                    loginDetails = new LoginDetail
                    {
                        Username = user.Username,
                        Token = jwt,
                        CreatedAt = DateTime.UtcNow
                    };
                    await _loginService.CreateLoginDetails(loginDetails);
                }
                else
                {
                    loginDetails.Token = jwt;
                    await _loginService.UpdateLoginDetails(loginDetails.Id, loginDetails);
                }
            }

            return new BaseResponse(StatusCodes.Status201Created, new MessageDTO(loginDetails!));
        }
        catch (OperationCanceledException e)
        {
            _logger.LogError($"CancellationToken Operation Exception!");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Exception is { ex.Message}");
            throw;
        }
    }
}