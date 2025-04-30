using VC.Services.Common;

namespace VC.Services;

public class Service : AggregateRoot<Guid>, IHasTenantId
{
    private List<EmployeeAssignment> _employeeAssignments = [];
    
    public Service(Guid id, Guid tenantId) : base(id)
    {
        Id = id;
        TenantId = tenantId;
    }
    
    public string Title { get; set; }

    public string Description { get; set; }

    /// <summary>
    /// Базовая цена.
    /// </summary>
    /// <remarks>
    /// Если цена у сотрудников различается, установите ту, по которой работает большинство.
    /// </remarks>
    public decimal BasePrice { get; set; }

    /// <summary>
    /// Общая длительность оказания услуги. 
    /// </summary>
    public TimeSpan BaseDuration { get; set; }

    public Guid? CategoryId { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid TenantId { get; set; }

    public List<Guid> RequiredResources { get; set; }
    
    public IReadOnlyCollection<EmployeeAssignment> EmployeeAssignments => _employeeAssignments.AsReadOnly();
    
    /// <summary>
    /// Назначить сотрудника с индивидуальными параметрами.
    /// </summary>
    public void AssignEmployee(Guid employeeId, decimal? customPrice, TimeSpan? customDuration)
    {
        var assignment = new EmployeeAssignment(
            employeeId,
            customPrice ?? BasePrice,
            customDuration ?? BaseDuration
        );

        _employeeAssignments.Add(assignment);
    }
}