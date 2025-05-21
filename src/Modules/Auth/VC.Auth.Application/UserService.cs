using System.Security.Cryptography;
using FluentResults;
using VC.Auth.Application.Models.Requests;
using VC.Auth.Constants;
using VC.Auth.Interfaces;
using VC.Auth.Models;
using VC.Auth.Repositories;

namespace VC.Auth.Application;

public class UserService(
    IUserRepository _userRepository,
    IEncrypt _encrypt,
    IJwtOptions jwtOptions,
    IWebCookie _webCookie) : IUserService
{
    private IUserService _userServiceImplementation;

    public async Task<Result> Register(RegisterRequest request)
    {
        var salt = GeneratePasswordSalt();

        var user = new User
        {
            Id = Guid.CreateVersion7(),
            Username = request.Username,
            Email = request.Email,
            PasswordHash = _encrypt.HashPassword(request.Password, salt),
            Salt = salt
        };

        await _userRepository.CreateAsync(user);

        return Result.Ok();
    }

    public async Task<Result<string>> Login(LoginRequest request)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        bool verified = user.PasswordHash == _encrypt.HashPassword(request.Password, user.Salt);

        if (user is not null && verified)
        {
            _webCookie.AddSecure(
                AuthConstants.RememberMeCookieName,
                jwtOptions.GenerateToken(user),
                AuthConstants.RememberMeDays);
        }

        return Result.Ok();
    }

    public async Task Logout()
        => await _webCookie.Delete(AuthConstants.RememberMeCookieName);

    private string GeneratePasswordSalt()
    {
        byte[] saltBytes = new byte[32];
        RandomNumberGenerator.Create().GetBytes(saltBytes);
        return Convert.ToBase64String(saltBytes);
    }
}