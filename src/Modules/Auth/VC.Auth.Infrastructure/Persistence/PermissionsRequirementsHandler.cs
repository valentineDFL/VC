using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using VC.Auth.Repositories;

namespace VC.Auth.Infrastructure.Persistence;

public class PermissionAuthorizationHandler(IServiceScopeFactory _serviceScopeFactory)
    : AuthorizationHandler<PermissionRequirements>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PermissionRequirements requirement)
    {
        var username = context.User.Claims.FirstOrDefault(
            c => c.Type == ClaimTypes.Name)!.ToString();

        using var scope = _serviceScopeFactory.CreateScope();

        var userRepository = scope.ServiceProvider.GetService<IUserRepository>();
        var permissions = userRepository.GetPermissionByUsername(username);
        if (permissions.Any(x => x.Name == requirement.Permission))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}