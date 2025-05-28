using Microsoft.EntityFrameworkCore;
using VC.Auth.Infrastructure.Persistence.DataContext;
using VC.Auth.Models;
using VC.Auth.Repositories;

namespace VC.Auth.Infrastructure.Persistence.Repositories;

public class UserRepository(AuthDbContext _dbContext) : IUserRepository
{
    public async Task<User> GetByEmailAsync(string email)
        => await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task CreateAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<ICollection<Permission>> GetPermissionsByUsernameAsync(string username)
    {
        var user = await _dbContext.Users
            .Include(p => p.Permissions)
            .FirstOrDefaultAsync(u => u.Username == username);

        if (user is null)
            return [];

        return user.Permissions;
    }
}