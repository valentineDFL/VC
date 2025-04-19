namespace VC.Tenants.Api.Models.Request.Schedule;

public record CreateDayScheduleDto(DayOfWeek Day, DateTime StartWork, DateTime EndWork);