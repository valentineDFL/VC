namespace VC.Core.Common;

public interface IHasTenantId
{
    public Guid TenantId { get; }
}