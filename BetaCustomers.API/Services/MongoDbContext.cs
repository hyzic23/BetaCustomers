using BetaCustomers.API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BetaCustomers.API.Services;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    private readonly string _playlistCollectionName;

    public MongoDbContext(IOptions<MongoDbConfig> mongoDbSettings)
    {
        var client = new MongoClient(mongoDbSettings.Value.ConnectionUri);
        _database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _playlistCollectionName = mongoDbSettings.Value.PlaylistCollectionName;
    }
    
    public IMongoCollection<Playlist> PlaylistCollections =>
                        _database.GetCollection<Playlist>(_playlistCollectionName);
    
}