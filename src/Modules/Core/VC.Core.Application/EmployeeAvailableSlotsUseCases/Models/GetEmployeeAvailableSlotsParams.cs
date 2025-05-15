namespace VC.Core.Application.EmployeeAvailableSlotsUseCases.Models;

public record GetEmployeeAvailableSlotsParams(Guid EmployeeId, DateOnly Date);