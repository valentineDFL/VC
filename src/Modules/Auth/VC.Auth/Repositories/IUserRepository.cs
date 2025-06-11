using VC.Auth.Models;

namespace VC.Auth.Repositories;

public interface IUserRepository
{
    Task<User> GetByIdAsync(Guid id);

    Task<User> GetByEmailAsync(string email);

    Task<ICollection<Permission>> GetPermissionsByUsernameAsync(string username);

    Task CreateAsync(User user);

    Task UpdateAsync(User user);
}