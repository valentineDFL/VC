namespace VC.Tenants.Entities;

public class TenantWorkSchedule
{
    // добавить модели коллекций Id, 

    private List<TenantDayWorkSchedule> _daysSchedule;

    private TenantWorkSchedule(List<TenantDayWorkSchedule> daysSchedule)
    {
        _daysSchedule = daysSchedule;
    }

    private TenantWorkSchedule() { }

    public IReadOnlyList<TenantDayWorkSchedule> DaysSchedule => _daysSchedule;

    public static TenantWorkSchedule Create(List<TenantDayWorkSchedule> dayWorkSchedules)
    {
        if (dayWorkSchedules.Count != 7)
            throw new ArgumentException("Day count must be 7");

        if (dayWorkSchedules.DistinctBy(x => x.Day).Count() != 7)
            throw new ArgumentException("DayWork must be unique");

        return new TenantWorkSchedule(dayWorkSchedules);
    }
}
