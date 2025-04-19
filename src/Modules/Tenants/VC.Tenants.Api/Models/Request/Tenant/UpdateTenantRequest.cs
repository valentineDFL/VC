using VC.Tenants.Api.Models.Request.Schedule;
using VC.Tenants.Api.Models.Transfer;
using VC.Tenants.Entities;

namespace VC.Tenants.Api.Models.Request.Tenant;

public record UpdateTenantRequest
    (string Name,
     string Slug,
     TenantConfigurationDto Config,
     TenantStatus Status,
     ContactInfoDto ContactInfo,
     IReadOnlyList<UpdateDayScheduleDto> WorkSchedule);