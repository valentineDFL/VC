using VC.Services.Common;

namespace VC.Services;

/// <summary>
/// Сотрудник.
/// </summary>
public class Employee : AggregateRoot<Guid>, IHasTenantId
{
    public Employee(Guid id, Guid tenantId) : base(id)
    {
        TenantId = tenantId;
    }

    public string FullName { get; set; }
    
    /// <summary>
    /// Название специализации (увидят клиенты при записи).
    /// </summary>
    public string Specialisation { get; set; }
    
    public Guid TenantId { get; }
}