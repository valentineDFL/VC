using VC.Auth.Interfaces;
using VC.Auth.Models;
using VC.Shared.Utilities.Constants;

namespace VC.Auth.Infrastructure.Implementations;

internal class JwtClaimsGenerator : IJwtClaimsGenerator
{
    public async Task<Dictionary<string, string>> GenerateClaimsByUserAsync(User user)
    {
        var result = new Dictionary<string, string>()
        {
            [JwtClaimTypes.UserId] = user.Id.ToString(),
            [JwtClaimTypes.Username] = user.Username,
            [JwtClaimTypes.Email] = user.Email,
        };

        if (user.Permissions.All(p => p.Name is not Permissions.Tenant))
            return result;

        result.Add(JwtClaimTypes.TenantId, user.TenantId.ToString());

        return result;
    }

    public Dictionary<string, string> GenerateRefreshTokenClaims(DateTime lifeTime, Guid userId)
    {
        var result = new Dictionary<string, string>()
        {
            [JwtClaimTypes.RefreshToken] = JwtClaimTypes.RefreshToken,
            [JwtClaimTypes.RefreshTokenLifeTime] = lifeTime.ToString(),
            [JwtClaimTypes.UserId] = userId.ToString(),
        };

        return result;
    }
}