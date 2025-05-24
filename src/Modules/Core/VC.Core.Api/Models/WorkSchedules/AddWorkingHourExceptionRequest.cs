
namespace VC.Core.Api.Models.WorkSchedules;

public record AddWorkingHourExceptionRequest(
    DateOnly Date,
    bool IsDayOff,
    TimeOnly? StartTime,
    TimeOnly? EndTime);