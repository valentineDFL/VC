namespace VC.Tenants.Api.Models.Request.Update;

public record UpdateDayScheduleDto
    (Guid Id,
     DayOfWeek Day,
     DateTime StartWork,
     DateTime EndWork);