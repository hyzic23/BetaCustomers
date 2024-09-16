namespace BetaCustomers.API.Models;

public class UserModel
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
}