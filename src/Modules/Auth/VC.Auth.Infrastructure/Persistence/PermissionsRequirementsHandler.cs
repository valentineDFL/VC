using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using VC.Auth.Repositories;

namespace VC.Auth.Infrastructure.Persistence;

public class PermissionAuthorizationHandler(IServiceScopeFactory _serviceScopeFactory)
    : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var username = context.User.Claims.FirstOrDefault(
            c => c.Type is ClaimTypes.Name)!.Value;

        using var scope = _serviceScopeFactory.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

        var permissions = await userRepository.GetPermissionsByUsernameAsync(username);

        if (permissions.Any(x => x.Name == requirement.Permission))
            context.Succeed(requirement);
    }
}   