using BetaCustomers.API.Config;
using BetaCustomers.API.IServices;
using BetaCustomers.API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BetaCustomers.API.Services;

public class UsersService : IUsersService
{
    private readonly MongoDbContext _context;
    private readonly HttpClient _httpClient;
    private readonly UsersApiConfig _apiConfig;
    
    public UsersService(HttpClient httpClient,
                        IOptions<UsersApiConfig> apiConfig,
                        IOptions<MongoDbConfig> mongoDbConfig
                        )
    {
        _httpClient = httpClient;
        _apiConfig = apiConfig.Value;
        _context = new MongoDbContext(mongoDbConfig);
    }

    public async Task<UserModel> CreateUser(UserModel user)
    {
        await _context.UserCollections.InsertOneAsync(user);
        return user;
    }

    public async Task<UserModel> GetUserById(string id)
    {
        var user = await _context
            .UserCollections
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync();
        return user;
    }

    public async Task<IEnumerable<UserModel>> GetUsers()
    {
        var users = await _context
            .UserCollections
            .Find(new BsonDocument())
            .ToListAsync();
        return users;
    }

    public async Task<UserModel> UpdateUser(string id, UserModel user)
    {
        await _context.UserCollections.ReplaceOneAsync(um => um.Id == id, user);
        return user;
    }

    public async Task DeleteUser(string id)
    {
        var filter = Builders<UserModel>.Filter.Eq("Id", id);
        await _context.UserCollections.DeleteOneAsync(filter);
    }

    public async Task<UserModel> CheckIfUserExist(string username)
    {
        var user = _context
            .UserCollections
            .Find(x => x.Username == username)
            .FirstOrDefault();
        return user;
    }

    public async Task<UserModel> ValidateUserCredential(string username, string password)
    {
        var user = await _context
            .UserCollections
            .Find(x => x.Username == username && x.Password == password)
            .FirstOrDefaultAsync();
        return user;
    }

    public async Task<List<User>>  GetAllUsers()
    {
        var usersResponse = await _httpClient.GetAsync(_apiConfig.Endpoint);
        
        if (usersResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return new List<User>();
        }

        var responseContent = usersResponse.Content;
        var allUsers = await responseContent.ReadFromJsonAsync<List<User>>();
        return allUsers.ToList();
    }
}