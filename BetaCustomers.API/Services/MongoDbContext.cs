using BetaCustomers.API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BetaCustomers.API.Services;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    private readonly string _playlistCollectionName;
    private readonly string _usersCollectionName;
    private readonly string _loginDetailsCollectionName;

    public MongoDbContext(IOptions<MongoDbConfig> mongoDbSettings)
    {
        var client = new MongoClient(mongoDbSettings.Value.ConnectionUri);
        _database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _playlistCollectionName = mongoDbSettings.Value.PlaylistCollectionName;
        _usersCollectionName = mongoDbSettings.Value.UserCollectionName;
        _loginDetailsCollectionName = mongoDbSettings.Value.LoginDetailsCollectionName;
    }
    
    public IMongoCollection<Playlist> PlaylistCollections =>
                        _database.GetCollection<Playlist>(_playlistCollectionName);
    public IMongoCollection<UserModel> UserCollections =>
        _database.GetCollection<UserModel>(_usersCollectionName);
    public IMongoCollection<LoginDetail> LoginDetailCollections =>
        _database.GetCollection<LoginDetail>(_loginDetailsCollectionName);
    
}