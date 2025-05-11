using FluentResults;
using VC.Core.Application.WorkScheduleUseCases.Models;
using VC.Core.Repositories;

namespace VC.Core.Application.WorkScheduleUseCases;

public interface IGetWorkScheduleDetailsUseCase : IUseCase<Guid, Result<WorkScheduleDto>>;

public class GetWorkScheduleDetailsUseCase(IUnitOfWork _unitOfWork) : IGetWorkScheduleDetailsUseCase
{
    public async Task<Result<WorkScheduleDto>> ExecuteAsync(Guid employeeId, CancellationToken ct = default)
    {
        var schedule = await _unitOfWork.WorkSchedules.GetByEmployeeAsync(employeeId, ct);
        if (schedule is null)
            return Result.Fail<WorkScheduleDto>("График работы не найден");

        var dto = new WorkScheduleDto(
            Id: schedule.Id,
            EmployeeId: schedule.EmployeeId,
            Items: schedule.Items.Select(i => new WorkScheduleItemDto(i.DayOfWeek, i.StartTime, i.EndTime)).ToList(),
            Exceptions: schedule.Exceptions.Select(e => new WorkScheduleExceptionDto(
                e.Date, e.IsDayOff, e.StartTime, e.EndTime)).ToList());

        return Result.Ok(dto);
    }
}