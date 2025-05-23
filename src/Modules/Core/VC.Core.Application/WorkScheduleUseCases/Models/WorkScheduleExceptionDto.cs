namespace VC.Core.Application.WorkScheduleUseCases.Models;

public record WorkScheduleExceptionDto(DateOnly Date, bool IsDayOff, TimeOnly? StartTime, TimeOnly? EndTime);