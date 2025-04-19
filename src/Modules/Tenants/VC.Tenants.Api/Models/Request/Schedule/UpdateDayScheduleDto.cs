namespace VC.Tenants.Api.Models.Request.Schedule;

public record UpdateDayScheduleDto(Guid Id, DayOfWeek Day, DateTime StartWork, DateTime EndWork);