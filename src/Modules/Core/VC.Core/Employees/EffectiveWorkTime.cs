namespace VC.Core.Employees;

/// <summary>
/// Описывает концепцию эффективного рабочего времени сотрудника.
/// </summary>
public abstract record EffectiveWorkTime;

/// <summary>
/// Сотрудник не работает в указанный день.
/// </summary>
public sealed record DayOff : EffectiveWorkTime;

/// <summary>
/// Сотрудник работает в указанный день по своему графику.
/// </summary>
/// <param name="StartTime">Время начала работы.</param>
/// <param name="EndTime">Время окончания работы.</param>
public sealed record Working(TimeOnly StartTime, TimeOnly EndTime) : EffectiveWorkTime;