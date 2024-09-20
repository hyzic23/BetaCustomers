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

    public async Task<LoginDetail> CreateLoginDetails(LoginDetail loginDetail)
    {
        await _context.LoginDetailCollections.InsertOneAsync(loginDetail);
        return loginDetail;
    }

    public async Task<LoginDetail> GetLoginDetails(string userId)
    {
        var loginDetails = await _context
            .LoginDetailCollections
            .Find(x => x.UserId == userId)
            .FirstOrDefaultAsync();
        return loginDetails;
    }
}