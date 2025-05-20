using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using VC.Auth.Models;
using VC.Auth.Repositories;

namespace VC.Auth.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AuthDatabaseContext _dbContext;

    public UserRepository(AuthDatabaseContext dbContext)
        => _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public async Task<User> GetByEmailAsync(string email)
    {
        if (_dbContext == null)
            throw new InvalidOperationException("DB context is not initialized");

        var allUsers = await _dbContext.Users.ToListAsync(); 
        Console.WriteLine($"All users: {JsonSerializer.Serialize(allUsers)}");

        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

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