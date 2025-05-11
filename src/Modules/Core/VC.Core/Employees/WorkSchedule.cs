using VC.Core.Common;

namespace VC.Core.Employees;

/// <summary>
/// Рабочий график сотрудника.
/// </summary>
public class WorkSchedule : AggregateRoot<Guid>, IHasTenantId
{
    private readonly List<WorkScheduleItem> _items = [];
    private readonly List<WorkingHourException> _exceptions = [];

    public WorkSchedule(Guid id, Guid employeeId, Guid tenantId) : base(id)
    {
        EmployeeId = employeeId;
        TenantId = tenantId;
    }

    public Guid EmployeeId { get; private set; }
    public Guid TenantId { get; private set; }

    public IReadOnlyCollection<WorkScheduleItem> Items => _items.AsReadOnly();
    public IReadOnlyCollection<WorkingHourException> Exceptions => _exceptions.AsReadOnly();
    
    /// <summary>
    /// Добавить или обновить элемент графика на указанный день недели.
    /// </summary>
    public void SetItem(WorkScheduleItem item)
    {
        var existingItem = _items.FirstOrDefault(i => i.DayOfWeek == item.DayOfWeek);
        if (existingItem is null)
        {
            _items.Add(item);
            return;
        }

        _items.Remove(item);
        _items.Add(item);
    }
    
    /// <summary>
    /// Добавить исключение.
    /// </summary>
    public void AddException(WorkingHourException exception)
    {
        if (_exceptions.Any(e => e.Date == exception.Date))
            throw new DomainException($"Исключение на дату {exception.Date} уже существует");

        _exceptions.Add(exception);
    }
    
    /// <summary>
    /// Получить эффективное рабочее время сотрудника на указанную дату.
    /// </summary>
    /// <param name="date">Дата на которую производится расчет эффективного рабочего времени.</param>
    public EffectiveWorkTime GetEffectiveWorkTime(DateOnly date)
    {
        var dayOfWeek = date.ToDateTime(TimeOnly.MinValue).DayOfWeek;

        var item = _items.FirstOrDefault(i => i.DayOfWeek == dayOfWeek);
        if (item is null)
            return new DayOff();

        var exception = _exceptions.FirstOrDefault(e => e.Date == date);
        if (exception?.IsDayOff == true)
            return new DayOff();

        var startTime = exception?.StartTime ?? item.StartTime;
        var endTime = exception?.EndTime ?? item.EndTime;

        return new Working(startTime, endTime);
    }
}
