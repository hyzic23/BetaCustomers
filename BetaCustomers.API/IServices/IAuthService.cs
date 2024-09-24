using BetaCustomers.API.Models;

namespace BetaCustomers.API.IServices;

public interface IAuthService
{
    ///  <summary>
    ///  Method is used to Authenticate request 
    ///  </summary>
    ///  <param name="request"></param>
    ///  <returns>BaseResponse</returns>
    Task<BaseResponse> AuthenticateUser(AuthenticateRequest request);
}