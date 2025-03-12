namespace VC.Tenants.Application.Tenants.Models;

public class ContactDto(string Email, string Phone, string Address)
{
    public string Email { get; } = Email;
    public string Phone { get; } = Phone;
    public string Address { get; } = Address;
}