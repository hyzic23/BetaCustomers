using BetaCustomers.API.IServices;
using BetaCustomers.API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BetaCustomers.API.Services;

public class PlaylistService : IPlaylistService
{
    private readonly IMongoCollection<Playlist> _playlistCollection;

    public PlaylistService(IOptions<MongoDbSettings> mongoDbSettings)
    {
        var client = new MongoClient(mongoDbSettings.Value.ConnectionUri);
        var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        _playlistCollection = database.GetCollection<Playlist>(mongoDbSettings.Value.CollectionName);
    }
    
    public async Task CreateAsync(Playlist playlist)
    {
        await _playlistCollection.InsertOneAsync(playlist);
    }

    public async Task<List<Playlist>> GetAsync()
    {
        return await _playlistCollection.Find(new BsonDocument()).ToListAsync();
    }

    public Task<Playlist> GetPlaylistByIdAsync(string id)
    {
        //TODO:
        //Flesh out this implementaion
        throw new NotImplementedException();
    }

    public async Task AddToPlaylistAsync(string id, string movieId)
    {
        FilterDefinition<Playlist> filter = Builders<Playlist>.Filter.Eq("Id", id);
        UpdateDefinition<Playlist> update = Builders<Playlist>.Update.AddToSet<string>("items", movieId);
        await _playlistCollection.UpdateOneAsync(filter, update);
        return;
    }

    public async Task DeleteAsync(string id)
    {
        FilterDefinition<Playlist> filter = Builders<Playlist>.Filter.Eq("Id", id);
        await _playlistCollection.DeleteOneAsync(filter);
        return;
    }
}