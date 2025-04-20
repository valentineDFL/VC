using VC.Tenants.Api.Models.Transfer;

namespace VC.Tenants.Api.Models.Response;

public record ResponseContactInfo(string Email, string Phone, AddressDto Address, bool IsVerify);
