using BetaCustomers.API.Models;

namespace BetaCustomers.API.IServices;

public interface IPlaylistService
{
    Task CreateAsync(Playlist playlist);
    Task<List<Playlist>> GetAsync();
    Task<Playlist> GetPlaylistByIdAsync(string id);
    Task AddToPlaylistAsync(string id, string movieId);
    Task DeleteAsync(string id);
}