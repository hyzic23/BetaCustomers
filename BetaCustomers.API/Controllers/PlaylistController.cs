using BetaCustomers.API.IServices;
using BetaCustomers.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetaCustomers.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class PlaylistController : ControllerBase
{
    //private readonly MongoDbService _mongoDbService;
    private readonly IPlaylistService _playlistService;

    public PlaylistController(IPlaylistService playlistService)
    {
        _playlistService = playlistService;
    }
    
    [HttpGet] 
    [Route("GetAllPlaylist")]
    public async Task<IActionResult> GetAllPlaylistAsync()
    {
        var playlists = await _playlistService.GetAsync();
        if (playlists.Any())
        {
            return Ok(playlists);
        }
        return NotFound();
    }
    
    [HttpGet] 
    [Route("GetPlaylistById/{id}")]
    public async Task<IActionResult> GetPlaylistByIdAsync(string id)
    {
        var playlist = await _playlistService.GetPlaylistByIdAsync(id);
        return Ok(playlist);
    }
    
    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create(Playlist playlist)
    {
        await _playlistService.CreateAsync(playlist);
        return CreatedAtAction(nameof(Create), new { id = playlist.Id}, playlist);
    }
    
    [HttpPut]
    [Route("UpdatePlaylist/{id}")]
    public async Task<IActionResult> AddToPlayList(string id, string movieId)
    {
        await _playlistService.AddToPlaylistAsync(id, movieId);
        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _playlistService.DeleteAsync(id);
        return NoContent();
    }
}