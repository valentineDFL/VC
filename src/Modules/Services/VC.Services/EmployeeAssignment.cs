using VC.Services.Common;

namespace VC.Services;

/// <summary>
/// Назначение сотрудника на услугу
/// </summary>
public class EmployeeAssignment : ValueObject
{
    public Guid EmployeeId { get; private set; }
    public decimal Price { get; set; }
    public TimeSpan Duration { get; set; }

    public EmployeeAssignment(
        Guid employeeId,
        decimal price,
        TimeSpan duration)
    {
        if (price <= 0) throw new ArgumentException("Price must be positive.");
        if (duration <= TimeSpan.Zero) throw new ArgumentException("Duration must be positive.");

        EmployeeId = employeeId;
        Price = price;
        Duration = duration;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return EmployeeId;
    }
}