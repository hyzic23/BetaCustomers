using BetaCustomers.API.Models;

namespace BetaCustomers.API.IServices;

public interface IAuthService
{
    Task<BaseResponse> AuthenticateUser(AuthenticateRequest request);
}