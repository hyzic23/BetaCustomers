using BetaCustomers.API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BetaCustomers.API.Services;

public class MongoDbService
{
    private readonly IMongoCollection<Playlist> _playlistCollection;

    public MongoDbService(IOptions<MongoDbSettings> mongoDbSettings)
    {
        var client = new MongoClient(mongoDbSettings.Value.ConnectionUri);
        var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _playlistCollection = database.GetCollection<Playlist>(mongoDbSettings.Value.CollectionName);
    }
}