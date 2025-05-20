using System.Security.Cryptography;
using FluentResults;
using Microsoft.AspNetCore.Http;
using VC.Auth.Application.Models.Requests;
using VC.Auth.Interfaces;
using VC.Auth.Models;
using VC.Auth.Repositories;

namespace VC.Auth.Application;

public class UserService(
    IUserRepository _userRepository,
    IPasswordHashHandler _passwordHashHandler,
    IHttpContextAccessor _httpContextAccessor,
    IJwtOptions jwtOptions) : IUserService
{
    public async Task<Result> Register(RegisterRequest request)
    {
        var user = new User
        {
            TenantId = Guid.CreateVersion7(),
            Username = request.Username.Trim(),
            Email = request.Email.Trim().ToLowerInvariant(),
            PasswordHash = _passwordHashHandler.HashPassword(
                request.Password,
                GeneratePasswordSalt()
            )
        };

        await _userRepository.CreateAsync(user);

        return Result.Ok();
    }

    public async Task<Result<string>> Login(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is null)
            return Result.Fail("User not found");

        var inputPassword = _passwordHashHandler.HashPassword(request.Password, GeneratePasswordSalt());

        if (user.PasswordHash != inputPassword)
            return Result.Fail("Password is incorrect");

        var token = jwtOptions.GenerateToken(user);

        // hide the name "cookies"
        _httpContextAccessor.HttpContext?.Response.Cookies.Append("cookies", token);

        return Result.Ok(token);
    }

    public async Task<Result> Logout()
    {
        var context = _httpContextAccessor.HttpContext;

        if (context is not null)
            context.Response.Cookies.Delete("cookies");

        return Result.Ok();
    }

    private string GeneratePasswordSalt()
    {
        byte[] saltBytes = new byte[32];
        RandomNumberGenerator.Create().GetBytes(saltBytes);
        return Convert.ToBase64String(saltBytes);
    }
}