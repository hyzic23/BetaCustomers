using BetaCustomers.API.Models;

namespace BetaCustomers.API.IServices;

public interface IAuthenticateService
{
    Task<BaseResponse> AuthenticateUser(AuthenticateRequest request);
}