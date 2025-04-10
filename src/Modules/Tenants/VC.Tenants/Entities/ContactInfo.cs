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

    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }

    /// <summary>
    /// Подтверждена ли почта
    /// </summary>
    public bool IsVerify { get; set; }

    /// <summary>
    /// Время до которого ссылка действительна
    /// </summary>
    public DateTime ConfirmationTime { get; set; }

    /// <summary>
    /// Действительна ли ссылка
    /// </summary>
    public bool ConfirmationTimeExpired => ConfirmationTime < DateTime.UtcNow; 
}