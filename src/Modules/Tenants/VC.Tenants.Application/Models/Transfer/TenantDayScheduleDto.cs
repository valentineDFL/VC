using VC.Tenants.Entities;

namespace VC.Tenants.Application.Models.Transfer;

public class TenantDayScheduleDto(Guid Id, DayOfWeek Day, DateTime StartWork, DateTime EndWork);