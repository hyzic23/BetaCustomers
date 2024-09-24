using BetaCustomers.API.Models;
using MongoDB.Driver;

namespace BetaCustomers.API.IServices;

public interface ILoginService
{
    ///  <summary>
    ///  Method is used to create Login Details 
    ///  </summary>
    ///  <param name="loginDetail"></param>
    ///  <returns>LoginDetail</returns>
    Task<LoginDetail> CreateLoginDetails(LoginDetail loginDetail);
    
    ///  <summary>
    ///  Method is used to get Login Details using username
    ///  </summary>
    ///  <param name="username"></param>
    ///  <returns>LoginDetail</returns>
    Task<LoginDetail> GetLoginDetails(string username);
    
    ///  <summary>
    ///  Method is used to update Login Details using id and loginDetails
    ///  </summary>
    ///  <param name="id"></param>
    ///  <param name="loginDetail"></param>
    ///  <returns>ReplaceOneResult</returns>
    Task<ReplaceOneResult> UpdateLoginDetails(string id, LoginDetail loginDetail);
}