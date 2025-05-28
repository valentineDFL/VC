using FluentResults;
using Microsoft.Extensions.Options;
using VC.Auth.Application.Abstractions;
using VC.Auth.Application.Models;
using VC.Auth.Infrastructure.Persistence.Models;
using VC.Auth.Interfaces;
using VC.Auth.Models;
using VC.Auth.Repositories;

namespace VC.Auth.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IEncrypt _encrypt;
    private readonly IJwtOptions _jwtOptions;
    private readonly IWebCookie _webCookie;
    private readonly IPasswordSaltGenerator _passwordSaltGenerator;
    private readonly CookiesSettings _cookiesSettings;

    public AuthService(
        IUserRepository userRepository,
        IEncrypt encrypt,
        IJwtOptions jwtOptions,
        IWebCookie webCookie,
        IPasswordSaltGenerator passwordSaltGenerator,
        IOptions<CookiesSettings> cookiesSettings)
    {
        _userRepository = userRepository;
        _encrypt = encrypt;
        _jwtOptions = jwtOptions;
        _webCookie = webCookie;
        _passwordSaltGenerator = passwordSaltGenerator;
        _cookiesSettings = cookiesSettings.Value;
    }

    public async Task<Result> SignUpAsync(RegisterAuthParams authParams)
    {
        var salt = _passwordSaltGenerator.Generate();

        var user = new User
        {
            Id = Guid.CreateVersion7(),
            Username = authParams.Username,
            Email = authParams.Email,
            PasswordHash = _encrypt.HashPassword(authParams.Password, salt),
            Salt = salt,
            Permissions = new List<Permission>()
            {
                new Permission()
                {
                    Name = Permissions.User
                }
            }
        };

        await _userRepository.CreateAsync(user);

        return Result.Ok();
    }

    public async Task<Result> LoginAsync(LoginAuthParams authParams)
    {
        var user = await _userRepository.GetByEmailAsync(authParams.Email);
        if (user is null)
            return Result.Fail("Invalid username or password");

        bool verified = user.PasswordHash == _encrypt.HashPassword(authParams.Password, user.Salt);

        if (!verified)
            return Result.Fail("Invalid password");

        _webCookie.AddSecure(_cookiesSettings.RememberMeCookieName, _jwtOptions.GenerateToken(user));

        return Result.Ok();
    }

    public async Task<Result> LogoutAsync()
    {
        var result = _webCookie.DeleteAsync(_cookiesSettings.RememberMeCookieName);

        if (!result.IsCompletedSuccessfully)
            return Result.Fail("Logout failed");

        return Result.Ok();
    }
}