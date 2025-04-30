using VC.Services.Common;

namespace VC.Services;

/// <summary>
/// Назначение сотрудника на услугу
/// </summary>
public class EmployeeAssignment : ValueObject
{
    public Guid EmployeeId { get; }
    public decimal Price { get; }
    public TimeSpan Duration { get; }

    public EmployeeAssignment(
        Guid EmployeeId,
        decimal Price,
        TimeSpan Duration)
    {
        this.EmployeeId = EmployeeId;
        this.Price = Price;
        this.Duration = Duration;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return EmployeeId;
        yield return Price;
        yield return Duration;
    }
}