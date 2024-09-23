using BetaCustomers.API.Models;

namespace BetaCustomers.API.IServices;

public interface ILoginService
{
    Task<LoginDetail> CreateLoginDetails(LoginDetail loginDetail);
    Task<LoginDetail> GetLoginDetails(string username);
    //Task<LoginDetail> GetLoginDetails(string userId);
}