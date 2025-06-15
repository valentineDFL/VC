using VC.Auth.Models;

namespace VC.Auth.Interfaces;

public interface IJwtClaimsGenerator
{
    public Task<Dictionary<string, string>> GenerateClaimsByUserAsync(User user);

    public Dictionary<string, string> GenerateRefreshTokenClaims(DateTime lifeTime, Guid userId);
}