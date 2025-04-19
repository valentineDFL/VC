using VC.Tenants.Api.Models.Transfer;
using VC.Tenants.Entities;

namespace VC.Tenants.Api.Models.Response;

public record ResponseTenantDto
    (string Name,
     string Slug,
     TenantConfigurationDto Config,
     TenantStatus Status,
     ContactInfoDto Contact,
     List<ResponseScheduleDto> WorkSchedule);