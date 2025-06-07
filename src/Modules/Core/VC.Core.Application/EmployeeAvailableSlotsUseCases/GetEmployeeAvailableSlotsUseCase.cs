using FluentResults;
using System.Collections.ObjectModel;
using VC.Core.Application.EmployeeAvailableSlotsUseCases.Models;
using VC.Core.Employees;
using VC.Core.Repositories;

namespace VC.Core.Application.EmployeeAvailableSlotsUseCases;

internal class GetEmployeeAvailableSlotsUseCase(
    IUnitOfWork _unitOfWork,
    AvailableSlotsGenerator _availableSlotsGenerator) : IGetEmployeeAvailableSlotsUseCase
{
    public async Task<Result<IReadOnlyCollection<AvailableSlotDto>>> ExecuteAsync(
        GetEmployeeAvailableSlotsParams parameters,
        CancellationToken cancellationToken = default)
    {
        IReadOnlyCollection<AvailableSlotDto> emptyResult = new Collection<AvailableSlotDto>();

        var workSchedule = await _unitOfWork.WorkSchedules.GetByEmployeeAndDateAsync(parameters.EmployeeId, parameters.Date, cancellationToken);
        if (workSchedule is null)
            return Result.Ok(emptyResult);

        var effectiveTime = workSchedule.GetEffectiveWorkTime(parameters.Date);
        if (effectiveTime is DayOff)
            return Result.Ok(emptyResult);

        if (effectiveTime is not Working workingEffectiveTime)
            throw new ApplicationException($"Не обработанный вариант эффективности рабочего времени сотрудника. Type={nameof(effectiveTime)}");

        var service = await _unitOfWork.Services.GetByAssignedEmployeeIdAsync(parameters.EmployeeId);

        if(service is null)
            return Result.Fail("Turned Employee does not Assigned");

        var serviceDuration = service.BaseDuration;

        var slots = await _availableSlotsGenerator.GenerateSlotsAsync(
            parameters.Date,
            workingEffectiveTime.StartTime,
            workingEffectiveTime.EndTime,
            serviceDuration,
            parameters.EmployeeId);

        return slots.Select(s => new AvailableSlotDto(s.From, s.To)).ToArray();
    }
}