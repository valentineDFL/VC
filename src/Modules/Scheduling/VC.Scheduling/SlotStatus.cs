namespace VC.Scheduling;

/// <summary>
/// Статусы временного слота
/// </summary>
public enum SlotStatus
{
    /// <summary>
    /// Доступен для бронирования
    /// </summary>
    Available,
    
    /// <summary>
    /// Временно забронирован (ожидает оплаты)
    /// </summary>
    Reserved,
    
    /// <summary>
    /// Подтвержденная бронь
    /// </summary>
    Booked,
    
    /// <summary>
    /// Заблокирован администратором
    /// </summary>
    Blocked,
    
    /// <summary>
    /// Технический перерыв
    /// </summary>
    Maintenance,
    
    /// <summary>
    /// Слот был разделен на части
    /// </summary>
    Split
}