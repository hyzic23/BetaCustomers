namespace BetaCustomers.API.Models;

public class AuthenticateRequest
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}