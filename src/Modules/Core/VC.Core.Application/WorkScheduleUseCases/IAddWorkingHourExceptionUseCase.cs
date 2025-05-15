using FluentResults;
using VC.Core.Employees;
using VC.Core.Repositories;
using VC.Shared.Utilities.Resolvers;

namespace VC.Core.Application.WorkScheduleUseCases;

public record AddWorkingHourExceptionParams(
    Guid EmployeeId,
    DateOnly Date,
    bool IsDayOff,
    TimeOnly? StartTime,
    TimeOnly? EndTime);

public interface IAddWorkingHourExceptionUseCase : IUseCase<AddWorkingHourExceptionParams, Result>;

public class AddWorkingHourExceptionUseCase(
    IUnitOfWork _unitOfWork,
    ITenantResolver _tenantResolver) : IAddWorkingHourExceptionUseCase
{
    public async Task<Result> ExecuteAsync(AddWorkingHourExceptionParams parameters, CancellationToken cancellationToken = default)
    {
        var workSchedule = await _unitOfWork.WorkSchedules.GetByEmployeeAndDateAsync(
            parameters.EmployeeId,
            parameters.Date,
            cancellationToken);
        if (workSchedule is null)
            return Result.Fail("График работы не найден для указанной даты");

        var workingHourException = new WorkingHourException(
            Guid.CreateVersion7(),
            parameters.EmployeeId,
            parameters.Date,
            parameters.IsDayOff,
            parameters.StartTime,
            parameters.EndTime,
            _tenantResolver.Resolve());

        workSchedule.AddException(workingHourException);
        await _unitOfWork.CommitAsync(cancellationToken);
        
        return Result.Ok();
    }
}