using VC.Core.Repositories;
using VC.Core.Services;

namespace VC.Core.Employees;

public class AvailableSlotsGenerator
{
    private readonly IOrdersHistoryRepository _orders;

    public AvailableSlotsGenerator(IOrdersHistoryRepository orders)
    {
        _orders = orders;
    }

    public async Task<List<AvailableSlot>> GenerateSlotsAsync(
        DateOnly date,
        Working workingEffectiveTime,
        TimeSpan duration,
        Guid employeeId)
            => await GenerateSlotsAsync(date, workingEffectiveTime.StartTime, workingEffectiveTime.EndTime, duration, employeeId);
    
    public async Task<List<AvailableSlot>> GenerateSlotsAsync(
        DateOnly date,
        TimeOnly start,
        TimeOnly end,
        TimeSpan duration,
        Guid employeeId)
    {
        var slots = new List<AvailableSlot>();

        var current = date.ToDateTime(start);
        var finish = date.ToDateTime(end);

        var nonAvailableSlots = await _orders.GetAllByEmployeeIdAndDateAsync(employeeId, date);

        while (current + duration <= finish)
        {
            var nonAvailableSlot = nonAvailableSlots.FirstOrDefault(x =>
            {
                var itemTimeHour = TimeOnly.FromDateTime(x.ServiceTime).Hour;

                return itemTimeHour == current.Hour;
            });

            var isNonAvailableSlot = nonAvailableSlot is not null;

            if (!isNonAvailableSlot)
            {
                slots.Add(new AvailableSlot(current, current + duration));
                current = current.Add(duration);
                continue;
            }

            if (nonAvailableSlot!.State is not OrderState.Canceled)
            {
                current = current.Add(duration);
                continue;
            }

            var remainingTime = (current - DateTime.UtcNow);
            var remainingMinutes = (remainingTime.Hours * 60) + remainingTime.Minutes;
            var isOpenToBookSlot = remainingMinutes > Service.WindowForRecordingInCaseOfCancellationInMinutes;

            if (isOpenToBookSlot)
            {
                slots.Add(new AvailableSlot(current, current + duration));
                current = current.Add(duration);
                continue;
            }

            current = current.Add(duration);
        }

        return slots;
    }
}