using VC.Core.Common;

namespace VC.Core.Employees;

/// <summary>
/// Исключение из стандартного графика работы сотрудника.
/// </summary>
public class WorkingHourException : Entity<Guid>, IHasTenantId
{
    public WorkingHourException(
        Guid id,
        Guid employeeId,
        DateOnly date,
        bool isDayOff,
        TimeOnly? startTime,
        TimeOnly? endTime,
        Guid tenantId) : base(id)
    {
        switch (isDayOff)
        {
            case true when (startTime.HasValue || endTime.HasValue):
                throw new DomainException("Для выходного дня нельзя указывать время начала и окончания");
            case false when (!startTime.HasValue || !endTime.HasValue):
                throw new DomainException("Для рабочего дня нужно указать начало и конец");
        }

        Id = id;
        EmployeeId = employeeId;
        Date = date;
        IsDayOff = isDayOff;
        StartTime = startTime;
        EndTime = endTime;
        TenantId = tenantId;
    }
    
    /// <summary>
    /// Сотрудник, для которого предназначено это исключение из графика.
    /// </summary>
    public Guid EmployeeId { get; private set; }
    
    /// <summary>
    /// Дата, на которую действует исключение.
    /// </summary>
    public DateOnly Date { get; private set; }
    
    /// <summary>
    /// true — не работает весь день
    /// </summary>
    public bool IsDayOff { get; private set; }
    
    /// <summary>
    /// Начало рабочего времени.
    /// </summary>
    public TimeOnly? StartTime { get; private set; }
    
    /// <summary>
    /// Конец рабочего времени.
    /// </summary>
    public TimeOnly? EndTime { get; private set; }

    public Guid TenantId { get; private set; }
}