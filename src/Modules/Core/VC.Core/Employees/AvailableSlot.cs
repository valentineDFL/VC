namespace VC.Core.Employees;

/// <summary>
/// Доступный слот для записи к сотруднику.
/// </summary>
/// <param name="From">Время начала.</param>
/// <param name="To">Время окончания.</param>
public record AvailableSlot(DateTime From, DateTime To);