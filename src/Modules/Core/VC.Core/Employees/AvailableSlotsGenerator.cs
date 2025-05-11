namespace VC.Core.Employees;

public class AvailableSlotsGenerator
{
    public List<AvailableSlot> GenerateSlots(
        DateOnly date,
        Working workingEffectiveTime,
        TimeSpan duration)
        =>  GenerateSlots(date, workingEffectiveTime.StartTime, workingEffectiveTime.EndTime, duration);
    
    public List<AvailableSlot> GenerateSlots(
        DateOnly date,
        TimeOnly start,
        TimeOnly end,
        TimeSpan duration)
    {
        var slots = new List<AvailableSlot>();

        var current = date.ToDateTime(start);
        var finish = date.ToDateTime(end);

        while (current + duration <= finish)
        {
            slots.Add(new AvailableSlot(current, current + duration));
            current = current.Add(duration);
        }

        return slots;
    }
}