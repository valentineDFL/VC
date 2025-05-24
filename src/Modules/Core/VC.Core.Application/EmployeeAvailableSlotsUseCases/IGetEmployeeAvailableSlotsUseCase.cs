using VC.Core.Application.EmployeeAvailableSlotsUseCases.Models;
using VC.Core.Employees;
using VC.Core.Repositories;

namespace VC.Core.Application.EmployeeAvailableSlotsUseCases;

public interface IGetEmployeeAvailableSlotsUseCase : IUseCase<GetEmployeeAvailableSlotsParams, IReadOnlyCollection<AvailableSlotDto>>;

internal class GetEmployeeAvailableSlotsUseCase(
    IUnitOfWork _unitOfWork,
    AvailableSlotsGenerator _availableSlotsGenerator) : IGetEmployeeAvailableSlotsUseCase
{
    public async Task<IReadOnlyCollection<AvailableSlotDto>> ExecuteAsync(
        GetEmployeeAvailableSlotsParams parameters,
        CancellationToken cancellationToken = default)
    {
        var workSchedule = await _unitOfWork.WorkSchedules.GetByEmployeeAndDateAsync(parameters.EmployeeId, parameters.Date, cancellationToken);
        if (workSchedule is null)
            return [];

        var effectiveTime = workSchedule.GetEffectiveWorkTime(parameters.Date);
        if (effectiveTime is DayOff)
            return [];

        if (effectiveTime is not Working workingEffectiveTime)
            throw new ApplicationException($"Не обработанный вариант эффективности рабочего времени сотрудника. Type={nameof(effectiveTime)}");
        
        // TODO: Зависимость на выбранную услугу. https://ru.yougile.com/team/880985e54735/VClients#VC-31
        // Нужно переработать AvailableSlotsGenerator чтобы учитывать длительность выполнения услуги выбранным сотрудником.
        // Исходя из этого, AvailableSlotsGenerator скорее всего переименуется.
        var serviceDuration = TimeSpan.FromHours(1);
        var slots = _availableSlotsGenerator.GenerateSlots(
            parameters.Date,
            workingEffectiveTime.StartTime,
            workingEffectiveTime.EndTime,
            serviceDuration);
        
        return slots.Select(s => new AvailableSlotDto(s.From, s.To)).ToArray();
    }
} 