using BetaCustomers.API.IServices;
using BetaCustomers.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetaCustomers.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthenticateController : ControllerBase
{
    private readonly IAuthenticateService _authService;

    public AuthenticateController(IAuthenticateService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("authenticate-user")]
    public async Task<IActionResult> GenerateToken(AuthenticateRequest authUser)
    {
        var token = await _authService.AuthenticateUser(authUser);
        return Ok(token);
    }
}