using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using VC.Auth.Repositories;
using VC.Shared.Utilities.Constants;

namespace VC.Auth.Infrastructure.Persistence;

public class PermissionAuthorizationHandler(IServiceScopeFactory _serviceScopeFactory)
    : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        // В случае если IsAuthenticated false будет возвращен 401 код.
        if (!context.User.Identity.IsAuthenticated)
            return;

        var claims = context.User.Claims;

        var username = claims.FirstOrDefault(c => c.Type is JwtClaimTypes.Username)!.Value;

        using var scope = _serviceScopeFactory.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

        var permissions = await userRepository.GetPermissionsByUsernameAsync(username);

        if (permissions.Any(x => x.Name == requirement.Permission))
            context.Succeed(requirement);
    }
}