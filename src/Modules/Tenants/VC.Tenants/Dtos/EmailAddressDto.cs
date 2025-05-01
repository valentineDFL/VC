namespace VC.Tenants.Dtos;

public class EmailAddressDto
{
    public string Email { get; private set; }

    public bool IsConfirmed { get; private set; }
}