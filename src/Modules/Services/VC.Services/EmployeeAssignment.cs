using VC.Services.Common;

namespace VC.Services;

/// <summary>
/// Назначение сотрудника на услугу
/// </summary>
public class EmployeeAssignment : Entity<Guid>
{
    public Guid EmployeeId { get; private set; }
    public decimal Price { get; private set; }
    public TimeSpan Duration { get; private set; }

    public EmployeeAssignment(
        Guid id,
        Guid employeeId,
        decimal price,
        TimeSpan duration)
    {
        Id = id;
        EmployeeId = employeeId;
        Price = price;
        Duration = duration;
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice <= 0) throw new ArgumentException("Price must be positive.");
        Price = newPrice;
    }

    public void UpdateDuration(TimeSpan newDuration)
    {
        if (newDuration <= TimeSpan.Zero) throw new ArgumentException("Duration must be positive.");
        Duration = newDuration;
    }
}