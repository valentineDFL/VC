namespace VC.Services.Common;

public interface IHasTenantId
{
    public Guid TenantId { get; }
}