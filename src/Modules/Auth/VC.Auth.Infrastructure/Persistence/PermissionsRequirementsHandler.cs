using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using VC.Auth.Repositories;

namespace VC.Auth.Infrastructure.Persistence;

public class PermissionAuthorizationHandler(IServiceScopeFactory _serviceScopeFactory)
    : AuthorizationHandler<PermissionRequirements>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PermissionRequirements requirement)
    {
        var username = context.User.Claims.FirstOrDefault(
            c => c.Type == ClaimTypes.Name)!.Value;

        using var scope = _serviceScopeFactory.CreateScope();

        var userRepository = scope.ServiceProvider.GetService<IUserRepository>();
        var permissions = await userRepository.GetPermissionsByUsernameAsync(username);
        
        if (permissions.Any(x => x.Name == requirement.Permission))
        {
            context.Succeed(requirement);
        }
    }
}