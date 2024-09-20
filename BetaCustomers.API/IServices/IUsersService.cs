using BetaCustomers.API.Models;

namespace BetaCustomers.API.IServices;

public interface IUsersService
{
    Task<UserModel> CreateUser(UserModel user);
    Task<UserModel> GetUserById(string id);
    Task<IEnumerable<UserModel>> GetUsers();
    Task<UserModel> UpdateUser(string id, UserModel user);
    Task DeleteUser(string id);
    Task<UserModel> CheckIfUserExist(string username);
    Task<List<User>> GetAllUsers(); // Test purpose
}