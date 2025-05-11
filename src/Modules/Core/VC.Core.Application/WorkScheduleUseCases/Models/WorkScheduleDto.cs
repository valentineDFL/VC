namespace VC.Core.Application.WorkScheduleUseCases.Models;

public record WorkScheduleDto(
    Guid Id,
    Guid EmployeeId,
    IReadOnlyCollection<WorkScheduleItemDto> Items,
    IReadOnlyCollection<WorkScheduleExceptionDto> Exceptions);