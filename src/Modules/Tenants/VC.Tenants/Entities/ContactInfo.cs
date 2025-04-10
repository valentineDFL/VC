namespace VC.Tenants.Entities;

/// <summary>
/// Контактная информация.
/// </summary>
public class ContactInfo
{
    /// <summary>
    /// Время действия ссылки
    /// </summary>
    public const int LinkMinuteValidTime = 3;

    public string? Email { get; private set; }
    public string? Phone { get; private set; }
    public string? Address { get; private set; }

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
}