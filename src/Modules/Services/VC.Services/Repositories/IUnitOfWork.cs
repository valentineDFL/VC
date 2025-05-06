namespace VC.Services.Repositories;

public interface IUnitOfWork : IDisposable
{
    IServicesRepository Services { get; }
    IResourcesRepository Resources { get; }
    ICategoriesRepository Categories { get; }

    Task BeginTransactionAsync();

    Task CommitAsync();

    Task RollBackAsync();
}