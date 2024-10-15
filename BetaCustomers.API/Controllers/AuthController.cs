using BetaCustomers.API.IServices;
using BetaCustomers.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetaCustomers.API.Controllers;

[ApiController]
[Route("api/auth")]
//[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("authenticate")]
    public async Task<IActionResult> GenerateToken(AuthenticateRequest authUser, CancellationToken cancellationToken)
    {
        var token = await _authService.AuthenticateUser(authUser, cancellationToken);
        return Ok(token);
    }
}