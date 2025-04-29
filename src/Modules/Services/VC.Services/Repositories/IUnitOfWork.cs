namespace VC.Services.Repositories;

public interface IUnitOfWork
{
    public IResourcesRepository Resources { get; }
    public IServicesRepository Services { get; }
    
    void BeginTransaction();
    
    Task BeginTransactionAsync();

    void Commit();
    
    Task CommitAsync();

    void Rollback();
    
    Task RollbackAsync();
    
    void SaveChanges();
    
    Task SaveChangesAsync();
}