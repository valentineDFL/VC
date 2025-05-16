using VC.Auth.Repositories;

namespace VC.Auth.Application;

public class TenantContext : ITenantContext
{
    public string? CurrentTenant { get; set; }
}