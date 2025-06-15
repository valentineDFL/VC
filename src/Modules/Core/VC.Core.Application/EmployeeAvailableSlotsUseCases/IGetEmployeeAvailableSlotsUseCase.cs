using FluentResults;
using VC.Core.Application.EmployeeAvailableSlotsUseCases.Models;

namespace VC.Core.Application.EmployeeAvailableSlotsUseCases;

public interface IGetEmployeeAvailableSlotsUseCase : 
    IUseCase<GetEmployeeAvailableSlotsParams, Result<IReadOnlyCollection<AvailableSlotDto>>>;