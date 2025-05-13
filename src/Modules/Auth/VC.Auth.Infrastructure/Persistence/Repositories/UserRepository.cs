using VC.Auth.Models;
using VC.Auth.Repositories;

namespace VC.Auth.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private static IDictionary<string, User> _users = new Dictionary<string, User>();
    
    public async Task AddAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync()
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(string user, string password)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }
}