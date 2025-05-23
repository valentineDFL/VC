using VC.Auth.Models;

namespace VC.Auth.Repositories;

public interface IUserRepository
{
    Task<User> GetByEmailAsync(string email);

    Task CreateAsync(User? user);

   Task GetByUsernameAsync(string username);

    ICollection<Permission> GetPermissionByUsername(string username);
}