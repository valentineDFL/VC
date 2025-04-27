namespace VC.Tenants.Entities;

public class EmailAddress : ValueObject
{
    public const int EmailAddressMaxLength = 64;

    /// <summary>
    /// Время действия кода подтверждения
    /// </summary>
    public const int CodeMinuteValidTime = 3;

    private EmailAddress(string email, bool isVerify, string code, DateTime confirmationTime)
    {
        Email = email;
        IsVerify = isVerify;
        Code = code;
        ConfirmationTime = confirmationTime;
    }

    private EmailAddress() { }

    public string Email { get; private set; }

    public bool IsVerify { get; private set; }

    /// <summary>
    /// Код подтверждения почты
    /// </summary>
    public string Code { get; private set; }

    /// <summary>
    /// Время до которого код будет действителен
    /// </summary>
    public DateTime ConfirmationTime { get; private set; }

    /// <summary>
    /// Действителен ли код
    /// </summary>
    public bool ConfirmationTimeExpired => ConfirmationTime < DateTime.UtcNow;

    public static EmailAddress Create(string email, bool isVerify, string code, DateTime confirmationTime)
    {
        if (string.IsNullOrEmpty(email)) throw new ArgumentNullException("Email cannot be null or empty");

        if (email.Length > EmailAddressMaxLength)
            throw new ArgumentException($"EmailAddress length must be lowest than {EmailAddressMaxLength} or equals.");

        return new EmailAddress(email, isVerify, code, confirmationTime);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Email;
        yield return IsVerify;
        yield return Code;
        yield return ConfirmationTime;
    }
}