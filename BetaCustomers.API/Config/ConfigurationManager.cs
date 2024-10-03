namespace BetaCustomers.API.Config;

public static class ConfigurationManager
{
    public static IConfiguration AppSetting { get; }

    static ConfigurationManager()
    {
        AppSetting = new Microsoft.Extensions.Configuration
                .ConfigurationManager()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build();
    }
}