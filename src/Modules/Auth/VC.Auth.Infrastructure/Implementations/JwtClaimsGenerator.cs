using System.Text;
using VC.Auth.Interfaces;
using VC.Auth.Models;
using VC.Shared.Utilities.ApiClients.TenantsService.Interfaces;
using VC.Shared.Utilities.Constants;

namespace VC.Auth.Infrastructure.Implementations;

internal class JwtClaimsGenerator : IJwtClaimsGenerator
{
    private readonly ITenantsApiClient _tenantsApiClient;

    public JwtClaimsGenerator(ITenantsApiClient tenantsApiClient)
    {
        _tenantsApiClient = tenantsApiClient;
    }

    public async Task<Dictionary<string, string>> GenerateClaimsByUserAsync(User user)
    {
        var result = new Dictionary<string, string>()
        {
            [JwtClaimTypes.UserId] = user.Id.ToString(),
            [JwtClaimTypes.Username] = user.Username,
        };

        if (user.Permissions.All(p => p.Name is not Permissions.Tenant))
            return result;

        var getTenantIdResult = await _tenantsApiClient.GetIdByUserIdAsync(user.Id);
        if (getTenantIdResult.IsSuccess)
        {
            result.Add(JwtClaimTypes.TenantId, getTenantIdResult.Value.ToString());
            return result;
        }

        var errorsMessages = getTenantIdResult.Errors.Select(x => x.Message);
        var sb = new StringBuilder();
        foreach (var error in errorsMessages)
            sb.AppendLine(error.ToString());

        throw new Exception(sb.ToString());
    }
}