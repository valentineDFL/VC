using VC.Tenants.Application.Models.Transfer;
using VC.Tenants.Application.Models.Update;
using VC.Tenants.Entities;

namespace VC.Tenants.Application.Models.Update;

public class UpdateContactInfoDto(string email, string phone, AddressDto address, bool isVerify, DateTime confirmationTime)
{
    public string Email { get; } = email;

    public string Phone { get; } = phone;
    
    public AddressDto Address { get; } = address;

    public bool IsVerify { get; } = isVerify;

    public DateTime ConfirmationTime { get; } = confirmationTime;
}

internal static class UpdateContactInfoMapper
{
    public static ContactInfo ToEntity(this UpdateContactInfoDto dto)
        => ContactInfo.Create(dto.Email, dto.Phone, dto.Address.ToEntity(), dto.IsVerify, dto.ConfirmationTime);
}