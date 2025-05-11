using VC.Core.Application.EmployeesUseCases.Models;
using VC.Core.Repositories;

namespace VC.Core.Application.EmployeesUseCases;

public interface IGetEmployeesUseCase : IUseCase<Unit, IReadOnlyCollection<EmployeeDto>>;

public class GetEmployeesUseCase(IUnitOfWork _unitOfWork) : IGetEmployeesUseCase
{
    public async Task<IReadOnlyCollection<EmployeeDto>> ExecuteAsync(
        Unit parameters,
        CancellationToken cancellationToken = default)
    {
        var employees = await _unitOfWork.Employees.GetAllAsync(cancellationToken);
        
        return employees.Select(e => new EmployeeDto(e.Id, e.FullName, e.Specialisation)).ToArray();
    }
}