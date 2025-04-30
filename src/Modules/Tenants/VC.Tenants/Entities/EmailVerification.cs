namespace VC.Tenants.Entities;

public class EmailVerification
{
    public const int CodeMinuteValidTime = 8;

    public const int CodeMaxLenght = 10;

    private EmailVerification(Guid id, Guid tenantId, EmailAddress email, string code, DateTime expirationTime, bool isConfirmed)
    {
        Id = id;
        TenantId = tenantId;
        Email = email;
        Code = code;
        ExpirationTime = expirationTime;
        IsConfirmed = isConfirmed;
    }

    public Guid Id { get; private set; }

    public Guid TenantId { get; private set; }

    public EmailAddress Email { get; private set; }

    /// <summary>
    /// Код подтверждения почты
    /// </summary>
    public string Code { get; private set; }

    /// <summary>
    /// Время до которого код будет действителен
    /// </summary>
    public DateTime ExpirationTime { get; private set; }

    public bool IsConfirmed { get; private set; }

    public bool ConfirmationTimeExpired => ExpirationTime < DateTime.UtcNow;

    public static EmailVerification Create(Guid id, Guid tenantId, EmailAddress email, string code, DateTime expirationTime, bool isConfirmed)
    {
        return new EmailVerification(id, tenantId, email, code, expirationTime, isConfirmed);
    }

    public void Confirm(string code)
    {
        if (IsConfirmed) throw new InvalidOperationException("Already confirmed.");
        if (DateTime.UtcNow > ExpirationTime) throw new InvalidOperationException("Code expired.");
        if (Code != code) throw new InvalidOperationException("Invalid code.");

        IsConfirmed = true;
    }
}