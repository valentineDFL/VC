using FluentResults;
using Microsoft.Extensions.Options;
using VC.Auth.Application.Abstractions;
using VC.Auth.Application.Models;
using VC.Auth.Interfaces;
using VC.Auth.Models;
using VC.Auth.Repositories;
using VC.Shared.Utilities.Options.Jwt;

namespace VC.Auth.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IJwtClaimsGenerator _jwtClaimsGenerator;

    private readonly IWebCookie _webCookie;
    private readonly CookiesSettings _cookiesSettings;

    private readonly IEncrypter _encrypter;
    private readonly IPasswordSaltGenerator _passwordSaltGenerator;

    public AuthService(
        IUserRepository userRepository,
        IEncrypter encrypter,
        IJwtTokenGenerator jwtTokenGenerator,
        IJwtClaimsGenerator claimsGenerator,
        IWebCookie webCookie,
        IPasswordSaltGenerator passwordSaltGenerator,
        IOptions<CookiesSettings> cookiesSettings)
    {
        _userRepository = userRepository;

        _encrypter = encrypter;
        _passwordSaltGenerator = passwordSaltGenerator;

        _jwtTokenGenerator = jwtTokenGenerator;
        _jwtClaimsGenerator = claimsGenerator;

        _webCookie = webCookie;
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
            PasswordHash = _encrypter.HashPassword(authParams.Password, salt),
            Salt = salt,
            Permissions = new List<Permission>()
            {
                new Permission()
                {
                    Id = Guid.CreateVersion7(),
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
            return Result.Fail("Not Found");

        bool verified = user.PasswordHash == _encrypter.HashPassword(authParams.Password, user.Salt);

        if (!verified)
            return Result.Fail("Invalid password");

        var jwtTokenClaims = await _jwtClaimsGenerator.GenerateClaimsByUserAsync(user);
        var jwtToken = _jwtTokenGenerator.GenerateToken(jwtTokenClaims);

        _webCookie.AddSecure(_cookiesSettings.RememberMeCookieName, jwtToken);

        return Result.Ok();
    }

    public async Task<Result> LogoutAsync()
    {
        var result = await _webCookie.DeleteAsync(_cookiesSettings.RememberMeCookieName);

        if (!result.IsSuccess)
            return Result.Fail(result.Errors);

        return Result.Ok();
    }
}