using VC.Core.Common;

namespace VC.Core.Employees;

/// <summary>
/// Сотрудник.
/// </summary>
public class Employee : AggregateRoot<Guid>, IHasTenantId
{
    public Employee(Guid id, Guid tenantId, string fullName) : base(id)
    {
        TenantId = tenantId;
        FullName = fullName;
    }
    
    public string FullName { get; set; }
    
    /// <summary>
    /// Название специализации (увидят клиенты при записи).
    /// </summary>
    public string? Specialisation { get; set; }
    
    public Guid TenantId { get; private set; }
}