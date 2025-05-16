namespace VC.Auth.Application;

public interface ITenantContext
{
    string CurrentTenant { get; set; }
}