using BetaCustomers.API.Models;

namespace BetaCustomers.API.IServices;

public interface IAuthenticateService
{
    BaseResponse AuthenticateUser(AuthenticateRequest request);
}