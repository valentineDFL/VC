namespace VC.Tenants.Entities;

/// <summary>
/// Контактная информация.
/// </summary>
public class ContactInfo
{
    public const int EmailAddressMaxLength = 64;

    public const int PhoneNumberMinLength = 15;
    public const int PhoneNumberMaxLength = 16;

    /// <summary>
    /// Время действия ссылки
    /// </summary>
    public const int LinkMinuteValidTime = 3;

    private ContactInfo(string? email, string? phone, Address address, bool isVerify, DateTime confirmationTime)
    {
        Email = email;
        Phone = phone;
        Address = address;
        IsVerify = isVerify;
        ConfirmationTime = confirmationTime;
    }

    private ContactInfo() { }

    public string? Email { get; private set; }

    public string? Phone { get; private set; }

    public Address Address { get; private set; }

    /// <summary>
    /// Подтверждена ли почта
    /// </summary>
    public bool IsVerify { get; private set; }

    /// <summary>
    /// Время до которого ссылка действительна
    /// </summary>
    public DateTime ConfirmationTime { get; private set; }

    /// <summary>
    /// Действительна ли ссылка
    /// </summary>
    public bool ConfirmationTimeExpired => ConfirmationTime < DateTime.UtcNow;

    public static ContactInfo Create(string? email, string? phone, Address address, bool isVerify, DateTime confirmationTime)
    {
        if (email.Length > EmailAddressMaxLength)
            throw new ArgumentException($"EmailAddress length must be lowest than {EmailAddressMaxLength} or equals.");

        if(address is null)
            throw new ArgumentNullException("Address cannot be null");

        return new ContactInfo(email, phone, address, isVerify, confirmationTime);
    }
}