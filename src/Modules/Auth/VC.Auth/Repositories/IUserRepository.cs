using VC.Auth.Models;

namespace VC.Auth.Repositories;

public interface IUserRepository
{
    Task<User> GetByEmailAsync(string email, string tenantId);

    Task CreateAsync(User user);
}