using StackExchange.Redis;

namespace BetaCustomers.API.Helper;

public class ConnectionHelper
{
    static ConnectionHelper()
    {
        ConnectionHelper.lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect(BetaCustomers.API.Config.ConfigurationManager.AppSetting["RedisURL"]);
        });
    }

    private static Lazy<ConnectionMultiplexer> lazyConnection;

    public static ConnectionMultiplexer Conection
    {
        get
        {
            return lazyConnection.Value;
        }
    }
}