using Mapster;
using VC.Tenants.Api.Models.Request.WeekSchedule;
using VC.Tenants.Api.Models.Transfer;
using VC.Tenants.Entities;

namespace VC.Tenants.Api.Models.Request.Tenant;

public record UpdateTenantRequest
    (string Name,
     string Slug,
     TenantConfigurationDto Config,
     TenantStatus Status,
     ContactInfoDto Contact,
     UpdateWeekScheduleDto WorkSchedule);

internal class UpdateTenantRequestConfig : IMapsterConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<UpdateTenantRequest, Application.Models.Update.UpdateTenantParams>.NewConfig()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Slug, src => src.Slug)
            .Map(dest => dest.TenantConfig, src => src.Config)
            .Map(dest => dest.TenantStatus, src => src.Status)
            .Map(dest => dest.Contact, src => src.Contact)
            .Map(dest => dest.WorkSchedule, src => src.WorkSchedule);
    }
}
