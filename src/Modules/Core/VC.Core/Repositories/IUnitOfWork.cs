namespace VC.Core.Repositories;

public interface IUnitOfWork : IDisposable
{
    IServicesRepository Services { get; }
    IResourcesRepository Resources { get; }
    ICategoriesRepository Categories { get; }
    IEmployeesRepository Employees { get; }
    IWorkSchedulesRepository WorkSchedules { get; }
    IOrdersHistoryRepository OrdersHistory { get; }
    
    
    Task BeginTransactionAsync();

    Task CommitAsync(CancellationToken cancellationToken = default);

    Task RollBackAsync();
}