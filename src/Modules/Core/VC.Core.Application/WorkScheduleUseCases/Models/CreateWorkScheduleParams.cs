using FluentResults;
using VC.Core.Employees;
using VC.Core.Repositories;
using VC.Shared.Utilities.Resolvers;

namespace VC.Core.Application.WorkScheduleUseCases.Models;

public record CreateWorkScheduleParams(
    Guid EmployeeId,
    IReadOnlyCollection<WorkScheduleItemDto> Items);

public interface ICreateWorkScheduleUseCase : IUseCase<CreateWorkScheduleParams, Result>;

public class CreateWorkScheduleUseCase(
    IUnitOfWork _unitOfWork,
    ITenantResolver _tenantResolver) : ICreateWorkScheduleUseCase
{
    public async Task<Result> ExecuteAsync(
        CreateWorkScheduleParams parameters,
        CancellationToken cancellationToken = default)
    {
        var employee = await _unitOfWork.Employees.GetByIdAsync(parameters.EmployeeId, cancellationToken);
        if (employee is null)
            return Result.Fail("Сотрудник не найден");

        var workSchedule = await _unitOfWork.WorkSchedules.GetByEmployeeAsync(parameters.EmployeeId, cancellationToken);
        if(workSchedule is not null)
            return Result.Fail("У сотрудника уже есть график работы");
        
        workSchedule = new WorkSchedule(
            Guid.CreateVersion7(),
            employeeId: parameters.EmployeeId,
            _tenantResolver.Resolve());
        
        foreach(var item in parameters.Items.Select(i => new WorkScheduleItem(i.DayOfWeek, i.StartTime, i.EndTime)))
            workSchedule.SetItem(item);
        
        await _unitOfWork.WorkSchedules.AddAsync(workSchedule, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Result.Ok();
    }
}