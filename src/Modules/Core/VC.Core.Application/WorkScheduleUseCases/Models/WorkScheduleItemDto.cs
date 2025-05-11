namespace VC.Core.Application.WorkScheduleUseCases.Models;

public record WorkScheduleItemDto(DayOfWeek DayOfWeek, TimeOnly StartTime, TimeOnly EndTime);