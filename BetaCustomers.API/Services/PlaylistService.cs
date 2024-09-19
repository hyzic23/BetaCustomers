using BetaCustomers.API.IServices;
using BetaCustomers.API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BetaCustomers.API.Services;

public class PlaylistService : IPlaylistService
{
    private readonly MongoDbContext _context;

    public PlaylistService(IOptions<MongoDbConfig> mongoDbSettings)
    {
        _context = new MongoDbContext(mongoDbSettings);
    }
    
    public async Task CreateAsync(Playlist playlist)
    {
        await _context.PlaylistCollections.InsertOneAsync(playlist);
    }

    public async Task<List<Playlist>> GetAsync()
    {
        return await _context.PlaylistCollections.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<Playlist> GetPlaylistByIdAsync(string id)
    {
        var playlist = await _context.PlaylistCollections.Find(x => x.Id == id).FirstOrDefaultAsync();
        return playlist;
    }
    
    public async Task AddToPlaylistAsync(string id, string movieId)
    {
        FilterDefinition<Playlist> filter = Builders<Playlist>.Filter.Eq("Id", id);
        UpdateDefinition<Playlist> update = Builders<Playlist>.Update.AddToSet<string>("items", movieId);
        await _context.PlaylistCollections.UpdateOneAsync(filter, update);
        return;
    }
    
    public async Task DeleteAsync(string id)
    {
        FilterDefinition<Playlist> filter = Builders<Playlist>.Filter.Eq("Id", id);
        await _context.PlaylistCollections.DeleteOneAsync(filter);
    }
}