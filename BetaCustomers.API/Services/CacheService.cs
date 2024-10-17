using BetaCustomers.API.Helper;
using BetaCustomers.API.IServices;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace BetaCustomers.API.Services;

public class CacheService : ICacheService
{
    private readonly IDatabase _db = ConnectionHelper.Conection.GetDatabase();

    /// <summary>
    /// Get Data using key
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public T GetData<T>(string key)
    {
        var value = _db.StringGet(key);
        if (!string.IsNullOrEmpty(value))
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
        return default;
    }

    /// <summary>
    /// Set Data with Value and Expiration Time of key
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="expirationTime"></param>
    /// <returns></returns>
    public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
    {
        TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
        var isSet = _db.StringSet(key, JsonConvert.SerializeObject(value), expiryTime);
        return isSet;
    }

    /// <summary>
    /// Remove Data
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
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