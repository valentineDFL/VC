namespace VC.Utilities;

public interface IHasTenant
{
    public Guid TenantId { get; set; }
}