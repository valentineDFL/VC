using Microsoft.AspNetCore.Authorization;

namespace VC.Auth;

public class PermissionRequirements : IAuthorizationRequirement
{
    public string Permission { get; }

    public PermissionRequirements(string user, string permission)
    {
        Permission = permission;
    }

    public PermissionRequirements()
    {
    }
}