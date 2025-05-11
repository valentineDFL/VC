using FluentResults;
using VC.Core.Application.EmployeesUseCases.Models;
using VC.Core.Employees;
using VC.Core.Repositories;
using VC.Utilities.Resolvers;

namespace VC.Core.Application.EmployeesUseCases;

public interface ICreateEmployeeUseCase : IUseCase<CreateEmployeeParams, Result>;

public class CreateEmployeeUseCase(IUnitOfWork _unitOfWork, ITenantResolver _tenantResolver) : ICreateEmployeeUseCase
{
    public async Task<Result> ExecuteAsync(CreateEmployeeParams parameters, CancellationToken cancellationToken = default)
    {
        var employee = new Employee(
            Guid.CreateVersion7(),
            _tenantResolver.Resolve(),
            parameters.FullName)
        {
            Specialisation = parameters.Specialisation
        };
        
        await _unitOfWork.Employees.AddAsync(employee, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        return Result.Ok();
    }
}