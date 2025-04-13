using Mapster;
using VC.Tenants.Api.Models.Transfer;
using VC.Tenants.Entities;
using VC.Tenants.Api.Models.Request.WeekSchedule;

namespace VC.Tenants.Api.Models.Request.Tenant;

public record CreateTenantRequest
    (string Name,
     string Slug,
     TenantConfigurationDto Config,
     TenantStatus Status,
     ContactInfoDto Contact,
     CreateWeekScheduleDto WorkSchedule);

internal class CreateTenantRequestConfig : IMapsterConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<CreateTenantRequest, Application.Models.Create.CreateTenantParams>.NewConfig()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Slug, src => src.Slug)
            .Map(dest => dest.TenantConfig, src => src.Config)
            .Map(dest => dest.TenantStatus, src => src.Status)
            .Map(dest => dest.Contact, src => src.Contact)
            .Map(dest => dest.WorkSchedule, src => src.WorkSchedule);
    }
}