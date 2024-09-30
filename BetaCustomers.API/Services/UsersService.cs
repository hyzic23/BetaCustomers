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
    private ILogger<UsersService> _logger;
    
    public UsersService(HttpClient httpClient,
                        IOptions<UsersApiConfig> apiConfig,
                        IOptions<MongoDbConfig> mongoDbConfig,
                        ILogger<UsersService> logger
                        )
    {
        _httpClient = httpClient;
        _apiConfig = apiConfig.Value;
        _context = new MongoDbContext(mongoDbConfig);
        _logger = logger;
    }

    ///  <summary>
    ///  Method is used to create user
    ///  </summary>
    ///  <param name="user"></param>
    ///  <returns>UserModel</returns>
    public async Task<UserModel> CreateUser(UserModel user)
    {
        await _context.UserCollections.InsertOneAsync(user);
        return user;
    }

    ///  <summary>
    ///  Method is used to get user by id
    ///  </summary>
    ///  <param name="id"></param>
    ///  <returns>UserModel</returns>
    public async Task<UserModel> GetUserById(string id)
    {
        var user = await _context
            .UserCollections
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync();
        return user;
    }

    ///  <summary>
    ///  Method is used to get all users
    ///  </summary>
    /// <param name="cancellationToken"></param>
    ///  <returns>UserModel</returns>
    public async Task<IEnumerable<UserModel>> GetUsers(CancellationToken cancellationToken)
    {
        IEnumerable<UserModel> users = null;
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            users = await _context
                .UserCollections
                .Find(new BsonDocument())
                .ToListAsync(cancellationToken);
        }
        catch (OperationCanceledException e)
        {
            _logger.LogError($"Operation for GetUsers was cancelled { e.CancellationToken } - { e.Message} !!!");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Exception occured for GetUsers with message { ex.Message } !!!");
            throw;
        }
        return users;
    }

    ///  <summary>
    ///  Method is used to update user
    ///  </summary>
    ///  <param name="id"></param>
    ///  <param name="user"></param>
    ///  <returns>UserModel</returns>
    public async Task<UserModel> UpdateUser(string id, UserModel user)
    {
        await _context.UserCollections.ReplaceOneAsync(um => um.Id == id, user);
        return user;
    }

    ///  <summary>
    ///  Method is used to delete user
    ///  </summary>
    ///  <param name="id"></param>
    ///  <returns></returns>
    public async Task DeleteUser(string id)
    {
        var filter = Builders<UserModel>.Filter.Eq("Id", id);
        await _context.UserCollections.DeleteOneAsync(filter);
    }

    ///  <summary>
    ///  Method is used to check if user exist using username
    ///  </summary>
    ///  <param name="username"></param>
    ///  <returns>UserModel</returns>
    public async Task<UserModel> CheckIfUserExist(string username)
    {
        var user = _context
            .UserCollections
            .Find(x => x.Username == username)
            .FirstOrDefault();
        return user;
    }

    ///  <summary>
    ///  Method is used to validate user's credential
    ///  </summary>
    ///  <param name="username"></param>
    ///  <param name="password"></param>
    ///  <returns>UserModel</returns>
    public async Task<UserModel> ValidateUserCredential(string username, string password)
    {
        var user = await _context
            .UserCollections
            .Find(x => x.Username == username && x.Password == password)
            .FirstOrDefaultAsync();
        return user;
    }

    ///  <summary>
    ///  Method is used for test purpose to fetch all users
    ///  </summary>
    ///  <returns></returns>
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