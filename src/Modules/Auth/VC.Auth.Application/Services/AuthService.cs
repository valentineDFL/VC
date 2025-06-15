using FluentResults;
using Microsoft.Extensions.Options;
using VC.Auth.Application.Abstractions;
using VC.Auth.Application.Models;
using VC.Auth.Interfaces;
using VC.Auth.Models;
using VC.Auth.Repositories;
using VC.Shared.Utilities.Constants;
using VC.Shared.Utilities.Options.Jwt;

namespace VC.Auth.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IJwtClaimsGenerator _jwtClaimsGenerator;
    private readonly IJwtTokenValidator _jwtTokenValidator;

    private readonly IWebCookie _webCookie;

    private readonly CookiesSettings _cookiesSettings;
    private readonly JwtSettings _jwtSettings;

    private readonly IEncrypter _encrypter;
    private readonly IPasswordSaltGenerator _passwordSaltGenerator;

    public AuthService(
        IUserRepository userRepository,
        IEncrypter encrypter,
        IJwtTokenGenerator jwtTokenGenerator,
        IJwtClaimsGenerator claimsGenerator,
        IJwtTokenValidator jwtTokenValidator,
        IWebCookie webCookie,
        IPasswordSaltGenerator passwordSaltGenerator,
        IOptions<CookiesSettings> cookiesSettings,
        IOptions<JwtSettings> jwtSetting)
    {
        _userRepository = userRepository;

        _encrypter = encrypter;
        _passwordSaltGenerator = passwordSaltGenerator;

        _jwtTokenGenerator = jwtTokenGenerator;
        _jwtClaimsGenerator = claimsGenerator;
        _jwtTokenValidator = jwtTokenValidator;

        _webCookie = webCookie;

        _cookiesSettings = cookiesSettings.Value;
        _jwtSettings = jwtSetting.Value;
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
        var jwtTokenLifetime = DateTime.UtcNow.AddMinutes(_jwtSettings.AuthTokenExpireMinutTime);
        var jwtToken = _jwtTokenGenerator.GenerateToken(jwtTokenClaims, jwtTokenLifetime);

        var refreshTokenLifetime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpireDays);
        var refreshTokenClaims = _jwtClaimsGenerator.GenerateRefreshTokenClaims(refreshTokenLifetime, user.Id);
        var refreshToken = _jwtTokenGenerator.GenerateToken(refreshTokenClaims, refreshTokenLifetime);

        _webCookie.AddSecure(_cookiesSettings.AuthCookieName, jwtToken);
        _webCookie.AddSecure(_cookiesSettings.RefreshTokenName, refreshToken);

        return Result.Ok();
    }

    public async Task<Result> RefreshTokenAsync()
    {
        var getRefreshTokenResult = await _webCookie.GetAsync(_cookiesSettings.RefreshTokenName);
        if (!getRefreshTokenResult.IsSuccess)
            return Result.Fail(getRefreshTokenResult.Errors);

        var refreshToken = getRefreshTokenResult.Value;

        var refreshTokenValidateResult = await _jwtTokenValidator.ValidateAsync(refreshToken);
        if (!refreshTokenValidateResult.IsValid)
            return Result.Fail("Token Non Valid!");

        var userIdString = (string)refreshTokenValidateResult.Claims[JwtClaimTypes.UserId];
        var userId = Guid.Parse(userIdString);

        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
            throw new NullReferenceException("User Not Found!");

        var jwtTokenClaims = await _jwtClaimsGenerator.GenerateClaimsByUserAsync(user);
        var jwtTokenLifetime = DateTime.UtcNow.AddMinutes(_jwtSettings.AuthTokenExpireMinutTime);
        var jwtToken = _jwtTokenGenerator.GenerateToken(jwtTokenClaims, jwtTokenLifetime);

        _webCookie.AddSecure(_cookiesSettings.AuthCookieName, jwtToken);

        return Result.Ok();
    }

    public async Task<Result> LogoutAsync()
    {
        var errors = new List<IError>();

        var deleteAccessTokenResult = await _webCookie.DeleteAsync(_cookiesSettings.AuthCookieName);
        var deleteRefreshTokenResult = await _webCookie.DeleteAsync(_cookiesSettings.RefreshTokenName);

        if (deleteAccessTokenResult.IsFailed)
            errors.AddRange(deleteAccessTokenResult.Errors);

        if (deleteRefreshTokenResult.IsFailed)
            errors.AddRange(deleteRefreshTokenResult.Errors);

        if (errors.Any())
            return Result.Fail(errors);

        return Result.Ok();
    }
}