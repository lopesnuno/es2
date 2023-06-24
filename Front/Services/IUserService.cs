using Front.Models.bModels;

namespace Front.Services;

public interface IUserService
{
    List<User> Users { get; set; }
    Task GetUsers();
    Task<User?> GetUserById(string id);

    Task UpdateUser(User user);
    Task CreateUser(User user);
    Task DeleteUser(string Id);
}