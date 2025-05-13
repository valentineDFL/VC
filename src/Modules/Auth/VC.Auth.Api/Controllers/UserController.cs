using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VC.Auth.Api.Handlers;
using VC.Auth.Infrastructure.Persistence;
using VC.Auth.Models;

namespace VC.Auth.Api.Controllers;

// [Route("api/[controller]/[action]")]
[Route("api/[controller]")]
[ApiController]
public class UserController(
    AuthDatabaseContext _dbContext,
    IPasswordHashHandler _passwordHash) : ControllerBase
{
    [HttpGet]
    public async Task<List<User>> Get()
        => await _dbContext.Users.ToListAsync();

    [HttpGet("{id}")]
    public async Task<User> GetById(Guid id)
        => await _dbContext.Users.FirstOrDefaultAsync(u => u.TenantId == id);

    [HttpPost]
    public async Task<ActionResult> Create(User user)
    {
        if (string.IsNullOrWhiteSpace(user.Username) ||
            string.IsNullOrWhiteSpace(user.Email) ||
            string.IsNullOrWhiteSpace(user.Password))
        {
            return BadRequest("Invalid request");
        }

        string salt = Guid.NewGuid().ToString("N");

        user.Password = _passwordHash.HashPassword(user.Password, salt);

        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = user.TenantId }, user);
    }

    [HttpPost]
    public async Task<ActionResult> Update(User user)
    {
        if (string.IsNullOrWhiteSpace(user.Username) ||
            string.IsNullOrWhiteSpace(user.Email) ||
            string.IsNullOrWhiteSpace(user.Password))
        {
            return BadRequest("Invalid request");
        }

        // захешировать пароль user.Password = 

        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = user.TenantId }, user);
    }
}