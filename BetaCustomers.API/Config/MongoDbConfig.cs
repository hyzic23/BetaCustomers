namespace BetaCustomers.API.Models;

public class MongoDbConfig
{
    public string ConnectionUri { get; set; } = null;
    public string DatabaseName { get; set; } = null;
    public string PlaylistCollectionName { get; set; } = null;
    public string UserCollectionName { get; set; } = null;
    public string LoginDetailsCollectionName { get; set; } = null;
}