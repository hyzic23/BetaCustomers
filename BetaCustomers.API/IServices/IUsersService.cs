using BetaCustomers.API.Models;

namespace BetaCustomers.API.IServices;

public interface IUsersService
{
    ///  <summary>
    ///  Method is used to create user
    ///  </summary>
    ///  <param name="user"></param>
    ///  <returns>UserModel</returns>
    Task<UserModel> CreateUser(UserModel user);
    
    ///  <summary>
    ///  Method is used to get user by id
    ///  </summary>
    ///  <param name="id"></param>
    ///  <returns>UserModel</returns>
    Task<UserModel> GetUserById(string id);
    
    ///  <summary>
    ///  Method is used to get all users
    ///  </summary>
    ///  <param name="cancellationToken"></param>
    ///  <returns>UserModel</returns>
    Task<IEnumerable<UserModel>> GetUsers(CancellationToken cancellationToken);
    
    ///  <summary>
    ///  Method is used to update user
    ///  </summary>
    ///  <param name="id"></param>
    ///  <param name="user"></param>
    ///  <returns>UserModel</returns>
    Task<UserModel> UpdateUser(string id, UserModel user);
    
    ///  <summary>
    ///  Method is used to delete user
    ///  </summary>
    ///  <param name="id"></param>
    ///  <returns></returns>
    Task DeleteUser(string id);
    
    ///  <summary>
    ///  Method is used to check if user exist using username
    ///  </summary>
    ///  <param name="username"></param>
    ///  <returns>UserModel</returns>
    Task<UserModel> CheckIfUserExist(string username);
    
    ///  <summary>
    ///  Method is used to validate user's credential
    ///  </summary>
    ///  <param name="username"></param>
    ///  <param name="password"></param>
    ///  <returns>UserModel</returns>
    Task<UserModel> ValidateUserCredential(string username, string password);
    
    ///  <summary>
    ///  Method is used for test purpose to fetch all users
    ///  </summary>
    ///  <returns></returns>
    Task<List<User>> GetAllUsers(); // Test purpose
}