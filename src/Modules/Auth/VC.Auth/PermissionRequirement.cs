using Microsoft.AspNetCore.Authorization;

namespace VC.Auth;

public class PermissionRequirement : IAuthorizationRequirement
{
    public string Permission { get; }

    public PermissionRequirement(string user, string permission)
    {
        Permission = permission;
    }
}