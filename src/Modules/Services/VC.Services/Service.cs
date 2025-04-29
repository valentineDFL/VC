using VC.Services.Common;

namespace VC.Services;

public class Service : AggregateRoot<Guid>, IHasTenantId
{
    private List<EmployeeAssignment> _employeeAssignments = [];
    private List<Guid> _requiredResources = [];
    
    public Service(
        Guid id,
        Guid tenantId,
        string title,
        decimal basePrice,
        TimeSpan baseDuration) : base(id)
    {
        TenantId = tenantId;
        Title = title;
        BasePrice = basePrice;
        BaseDuration = baseDuration;
    }
    
    public string Title { get; set; }

    public string? Description { get; set; }

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

    public Guid TenantId { get; set; }
    
    public IReadOnlyCollection<Guid> RequiredResources => _requiredResources.AsReadOnly();

    public IReadOnlyCollection<EmployeeAssignment> EmployeeAssignments => _employeeAssignments.AsReadOnly();
    
    /// <summary>
    /// Назначение или обновление сотрудника.
    /// </summary>
    public void AssignEmployee(Guid employeeId, decimal? customPrice, TimeSpan? customDuration)
    {
        var existingAssignment = _employeeAssignments.FirstOrDefault(a => a.EmployeeId == employeeId);
        
        if (existingAssignment != null)
        {
            existingAssignment.UpdatePrice(customPrice ?? BasePrice);
            existingAssignment.UpdateDuration(customDuration ?? BaseDuration);
            return;
        }

        var assignment = new EmployeeAssignment(
            id: Guid.CreateVersion7(),
            employeeId: employeeId,
            price: customPrice ?? BasePrice,
            duration: customDuration ?? BaseDuration
        );

        _employeeAssignments.Add(assignment);
    }
    
    public void UpdateEmployeeAssignment(Guid employeeId, decimal newPrice, TimeSpan newDuration)
    {
        var assignment = _employeeAssignments.FirstOrDefault(a => a.EmployeeId == employeeId)
                         ?? throw new DomainException("Employee not assigned.");

        assignment.UpdatePrice(newPrice);
        assignment.UpdateDuration(newDuration);
    }
    
    public void AddResource(Guid resourceId)
    {
        if (_requiredResources.Contains(resourceId))
            throw new DomainException("Resource already added.");

        _requiredResources.Add(resourceId);
    }
}