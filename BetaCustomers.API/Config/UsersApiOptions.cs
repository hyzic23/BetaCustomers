namespace BetaCustomers.API.Config;

public class UsersApiOptions
{
    public string Endpoint { get; set; }
    public string SecretKey { get; set; }
    public string ExpiryTimeInMinutes { get; set; } 
}