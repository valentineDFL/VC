namespace VC.Tenants.Application.Tenants.Models;

public class ContactDto(string email, string phone, string address)
{
    public string Email { get; } = email;
    public string Phone { get; } = phone;
    public string Address { get; } = address;
}