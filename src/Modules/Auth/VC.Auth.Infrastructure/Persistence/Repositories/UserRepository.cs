using Microsoft.EntityFrameworkCore;
using VC.Auth.Models;
using VC.Auth.Repositories;

namespace VC.Auth.Infrastructure.Persistence.Repositories;

public class UserRepository(AuthDatabaseContext _dbContext) : IUserRepository
{
    public async Task<User> GetByEmailAsync(string email)
        => await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task CreateAsync(User user)
    {
        if (string.IsNullOrWhiteSpace(user.Username) ||
            string.IsNullOrWhiteSpace(user.Email) ||
            string.IsNullOrWhiteSpace(user.PasswordHash))
        {
            throw new Exception("Some fields are empty");
        }

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }
}