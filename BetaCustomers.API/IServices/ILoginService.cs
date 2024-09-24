using BetaCustomers.API.Models;
using MongoDB.Driver;

namespace BetaCustomers.API.IServices;

public interface ILoginService
{
    Task<LoginDetail> CreateLoginDetails(LoginDetail loginDetail);
    Task<LoginDetail> GetLoginDetails(string username);
    Task<ReplaceOneResult> UpdateLoginDetails(string id, LoginDetail loginDetail);
}