using BetaCustomers.API.Helper;
using BetaCustomers.API.IServices;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace BetaCustomers.API.Services;

public class CacheService : ICacheService
{
    private readonly IDatabase _db = ConnectionHelper.Conection.GetDatabase();

    public T GetData<T>(string key)
    {
        var value = _db.StringGet(key);
        if (!string.IsNullOrEmpty(value))
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
        return default;
    }

    public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
    {
        TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
        var isSet = _db.StringSet(key, JsonConvert.SerializeObject(value), expiryTime);
        return isSet;
    }

    public object RemoveData(string key)
    {
        bool isKeyExist = _db.KeyExists(key);
        if (isKeyExist)
        {
            return _db.KeyDelete(key);
        }
        return false;
    }
}