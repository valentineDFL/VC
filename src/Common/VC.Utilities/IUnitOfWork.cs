namespace VC.Utilities;

public interface IUnitOfWork
{
    void BeginTransaction();

    void Commit();

    void Rollback();

    void SaveChanges();
}