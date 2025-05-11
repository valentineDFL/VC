using VC.Core.Application.WorkScheduleUseCases.Models;

namespace VC.Core.Api.Models.WorkSchedules;

public record CreateWorkScheduleRequest(IReadOnlyCollection<WorkScheduleItemDto> Items);