namespace VC.Utilities;

public interface IUnitOfWork
{
    void BeginTransaction();
    
    Task BeginTransactionAsync();

    void Commit();
    
    Task CommitAsync();

    void Rollback();
    
    Task RollbackAsync();
    
    void SaveChanges();
    
    Task SaveChangesAsync();
}