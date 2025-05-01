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
        var price = customPrice ?? BasePrice;
        var duration = customDuration ?? BaseDuration;

        var assignment = _employeeAssignments.FirstOrDefault(a => a.EmployeeId == employeeId);

        if (assignment is null)
        {
            _employeeAssignments.Add(new EmployeeAssignment(employeeId, price, duration));
            return;
        }
        
        if (assignment.Price == price && assignment.Duration == duration)
            return;

        _employeeAssignments.Remove(assignment);
        _employeeAssignments.Add(new EmployeeAssignment(employeeId, price, duration));
    }
    
    public void AddResource(Guid resourceId)
    {
        if (_requiredResources.Contains(resourceId))
            throw new DomainException("Resource already added.");

        _requiredResources.Add(resourceId);
    }

    public void RemoveAllResources() => _requiredResources.Clear();

    public void RemoveEmployeeAssignment(EmployeeAssignment assignment)
    {
        _employeeAssignments.Remove(assignment);
    }
}