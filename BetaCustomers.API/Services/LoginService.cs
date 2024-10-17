using BetaCustomers.API.IServices;
using BetaCustomers.API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BetaCustomers.API.Services;

public class LoginService : ILoginService
{
    private readonly MongoDbContext _context;

    public LoginService(IOptions<MongoDbConfig> mongoDbConfig)
    {
        _context = new MongoDbContext(mongoDbConfig);
    }

    ///  <summary>
    ///  Method is used to create Login Details 
    ///  </summary>
    ///  <param name="loginDetail"></param>
    ///  <returns>LoginDetail</returns>
    public async Task<LoginDetail> CreateLoginDetails(LoginDetail loginDetail)
    {
        await _context.LoginDetailCollections.InsertOneAsync(loginDetail);
        return loginDetail;
    }

    ///  <summary>
    ///  Method is used to get Login Details using username
    ///  </summary>
    ///  <param name="username"></param>
    ///  <returns>LoginDetail</returns>
    public async Task<LoginDetail> GetLoginDetails(string username)
    {
        var loginDetails = await _context
            .LoginDetailCollections
            .Find(x => x.Username == username)
            .FirstOrDefaultAsync() ?? new LoginDetail();
        return loginDetails;
    }

    ///  <summary>
    ///  Method is used to update Login Details using id and loginDetails
    ///  </summary>
    ///  <param name="id"></param>
    ///  <param name="loginDetail"></param>
    ///  <returns>ReplaceOneResult</returns>
    public async Task<ReplaceOneResult> UpdateLoginDetails(string id, LoginDetail loginDetail)
    {
        return await _context.LoginDetailCollections
            .ReplaceOneAsync(x => x.Id == id, loginDetail);
    }
}