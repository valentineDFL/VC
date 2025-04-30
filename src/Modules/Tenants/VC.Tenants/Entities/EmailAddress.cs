namespace VC.Tenants.Entities;

public class EmailAddress : ValueObject
{
    public const int EmailAddressMaxLength = 64;

    /// <summary>
    /// Время действия кода подтверждения
    /// </summary>
    public const int CodeMinuteValidTime = 8;

    private EmailAddress(string email)
    {
        Email = email;
    }

    private EmailAddress() { }

    public string Email { get; private set; }

    public static EmailAddress Create(string email)
    {
        if (string.IsNullOrEmpty(email)) throw new ArgumentNullException("Email cannot be null or empty");

        if (email.Length > EmailAddressMaxLength)
            throw new ArgumentException($"EmailAddress length must be lowest than {EmailAddressMaxLength} or equals.");

        return new EmailAddress(email);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Email;
    }
}