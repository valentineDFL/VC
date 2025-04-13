using Mapster;

namespace VC.Tenants.Api.Models.Transfer;

public record ContactInfoDto(string Email, string Phone, AddressDto Address);

internal class ContactInfoDtoMapperConfig : IMapsterConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<ContactInfoDto, Application.Models.Update.UpdateContactInfoDto>.NewConfig()
            .Map(dest => dest.Email, src => src.Phone)
            .Map(dest => dest.Phone, src => src.Phone)
            .Map(dest => dest.Address, src => src.Address);
    }
}