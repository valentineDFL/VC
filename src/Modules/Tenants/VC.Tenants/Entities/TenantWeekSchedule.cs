namespace VC.Tenants.Entities;

public class TenantWeekSchedule
{
    // добавить модели коллекций Id, 

    private List<TenantDaySchedule> _daysSchedule;

    private TenantWeekSchedule(List<TenantDaySchedule> daysSchedule)
    {
        _daysSchedule = daysSchedule;
    }

    private TenantWeekSchedule() { }

    public IReadOnlyList<TenantDaySchedule> DaysSchedule => _daysSchedule;

    public static TenantWeekSchedule Create(List<TenantDaySchedule> dayWorkSchedules)
    {
        if (dayWorkSchedules.Count != 7)
            throw new ArgumentException("Day count must be 7");

        if (dayWorkSchedules.DistinctBy(x => x.Day).Count() != 7)
            throw new ArgumentException("DayWork must be unique");

        return new TenantWeekSchedule(dayWorkSchedules);
    }
}
