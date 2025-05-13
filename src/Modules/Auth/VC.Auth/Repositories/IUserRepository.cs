
using VC.Auth.Models;

namespace VC.Auth.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user);

    Task GetByEmailAsync(string email);

    Task CreateAsync();

    Task SaveAsync();
}