using VC.Auth.Models;

namespace VC.Auth.Repositories;

public interface IUserRepository
{
    Task<User> GetByEmailAsync(string email);

    Task CreateAsync(User? user);
}