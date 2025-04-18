namespace VC.Scheduling;

/// <summary>
/// Настройки планирования ресурсов.
/// </summary>
public class SchedulingSettings : Entity<Guid>
{
    private TimeSpan _minLeadTime;
    private TimeSpan _maxBookingWindow;
    private TimeSpan _slotDuration;
    private bool _allowConcurrentBookings;
    private readonly List<DateTime> _blockedDates = [];
    private readonly Dictionary<ResourceType, string> _resourceTypeRules = new();

    public TimeSpan MinLeadTime
    {
        get => _minLeadTime;
        private set
        {
            if (value < TimeSpan.Zero)
                throw new DomainException("MinLeadTime cannot be negative", "INVALID_MIN_LEAD_TIME");
            _minLeadTime = value;
        }
    }

    public TimeSpan MaxBookingWindow
    {
        get => _maxBookingWindow;
        private set
        {
            if (value < TimeSpan.Zero)
                throw new DomainException("MaxBookingWindow cannot be negative", "INVALID_MAX_BOOKING_WINDOW");
            _maxBookingWindow = value;
        }
    }

    public TimeSpan SlotDuration
    {
        get => _slotDuration;
        private set
        {
            if (value < TimeSpan.FromMinutes(15))
                throw new DomainException("SlotDuration must be at least 15 minutes", "INVALID_SLOT_DURATION");
            _slotDuration = value;
        }
    }

    public bool AllowConcurrentBookings
    {
        get => _allowConcurrentBookings;
        private set => _allowConcurrentBookings = value;
    }

    public IReadOnlyList<DateTime> BlockedDates => _blockedDates.AsReadOnly();
    public IReadOnlyDictionary<ResourceType, string> ResourceTypeRules => _resourceTypeRules.AsReadOnly();

    public SchedulingSettings(
        Guid id,
        TimeSpan minLeadTime,
        TimeSpan maxBookingWindow,
        TimeSpan slotDuration,
        bool allowConcurrentBookings)
    {
        Id = id;
        MinLeadTime = minLeadTime;
        MaxBookingWindow = maxBookingWindow;
        SlotDuration = slotDuration;
        AllowConcurrentBookings = allowConcurrentBookings;
    }

    public void UpdateMinLeadTime(TimeSpan newValue) => MinLeadTime = newValue;

    public void AddBlockedDate(DateTime date)
    {
        if (_blockedDates.Contains(date))
            throw new DomainException("Date is already blocked", "DUPLICATE_BLOCKED_DATE");
        _blockedDates.Add(date);
    }

    public void AddResourceTypeRule(ResourceType type, string rule)
    {
        if (_resourceTypeRules.ContainsKey(type))
            throw new DomainException("Rule for this resource type already exists", "DUPLICATE_RESOURCE_RULE");
        _resourceTypeRules[type] = rule;
    }

    public void RemoveBlockedDate(DateTime date) => _blockedDates.Remove(date);
}