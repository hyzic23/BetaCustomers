using BetaCustomers.API.IServices;
using BetaCustomers.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetaCustomers.API.Controllers;

[ApiController]
[Route("api/[controller]")]
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
    public async Task<IActionResult> GenerateToken(AuthenticateRequest authUser)
    {
        var token = await _authService.AuthenticateUser(authUser);
        return Ok(token);
    }
}