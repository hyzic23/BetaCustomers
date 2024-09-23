using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BetaCustomers.API.Models;

public class LoginDetail
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    [BsonElement("Username")]
    public string Username { get; set; }
    [BsonElement("Token")]
    public string Token { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}