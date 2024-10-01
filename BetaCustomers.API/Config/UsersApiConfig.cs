namespace BetaCustomers.API.Config;

public class UsersApiConfig
{
    public string Endpoint { get; set; }
    public string SecretKey { get; set; }
    public string ExpiryTimeInMinutes { get; set; } 
    public string CachingExpiryTimeInMinutes { get; set; } 
}