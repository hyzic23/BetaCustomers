using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BetaCustomers.API.Models;

public class Playlist
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    public string Username { get; set; } = null!;
    
    [BsonElement("items")]
    [JsonPropertyName("items")]
    public List<string> MovieIds { get; set; } = null!;
}