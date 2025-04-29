namespace VC.Scheduling.BusinessRules;

/// <summary>
/// Минимальное время между текущим моментом и началом бронирования.
/// </summary>
/// <remarks>
///Для чего нужно MinLeadTimeRule? <br/>
///1. Предотвращение "последних минут" бронирования <br/>
///Например:<br/>
///- Клиент не может забронировать слот за 5 минут до его начала<br/>
///- Требуется время на подготовку ресурса (уборка кабинета, настройка оборудования)<br/>
///2. Технические ограничения <br/>
///- Время на обработку платежа<br/>
///- Синхронизация данных между системами<br/>
///3. Управление нагрузкой<br/>
///- Запрет бронирования "в реальном времени" для равномерного распределения заказов
/// </remarks>
public class MinLeadTimeRule : BusinessRule
{
    private readonly TimeSpan _minLeadTime;

    public MinLeadTimeRule(TimeSpan minLeadTime)
    {
        _minLeadTime = minLeadTime;
    }

    public override Task<bool> EvaluateAsync(
        TimeSlot slot, 
        SchedulingContext context, 
        CancellationToken ct = default)
    {
        var minAllowedStart = DateTime.UtcNow.Add(_minLeadTime);
        return Task.FromResult(slot.Start >= minAllowedStart);
    }
}