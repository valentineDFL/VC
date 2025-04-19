using VC.Tenants.Application.Models.Transfer;
using VC.Tenants.Application.Models.Update;
using VC.Tenants.Entities;

namespace VC.Tenants.Application.Models.Update;

public record UpdateContactInfoDto(string Email, string Phone, AddressDto Address, bool IsVerify, DateTime ConfirmationTime);

internal static class UpdateContactInfoMapper
{
    public static ContactInfo ToEntity(this UpdateContactInfoDto dto)
        => ContactInfo.Create(dto.Email, dto.Phone, dto.Address.ToEntity(), dto.IsVerify, dto.ConfirmationTime);
}