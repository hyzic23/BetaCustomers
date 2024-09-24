using BetaCustomers.API.Models;

namespace BetaCustomers.API.IServices;

public interface IPlaylistService
{
    ///  <summary>
    ///  Method is used to create playlist
    ///  </summary>
    ///  <param name="playlist"></param>
    ///  <returns></returns>
    Task CreateAsync(Playlist playlist);
    
    ///  <summary>
    ///  Method is used to get all playlist
    ///  </summary>
    ///  <returns>Playlist</returns>
    Task<List<Playlist>> GetAsync();
    
    ///  <summary>
    ///  Method is used to get playlist using id
    ///  </summary>
    ///  <param name="id"></param>
    ///  <returns>Playlist</returns>
    Task<Playlist> GetPlaylistByIdAsync(string id);
    
    ///  <summary>
    ///  Method is used to add movie to playlist
    ///  </summary>
    ///  <param name="id"></param>
    ///  <param name="movieId"></param>
    ///  <returns></returns>
    Task AddToPlaylistAsync(string id, string movieId);
    
    ///  <summary>
    ///  Method is used to delete playlist
    ///  </summary>
    ///  <param name="id"></param>
    ///  <returns></returns>
    Task DeleteAsync(string id);
}