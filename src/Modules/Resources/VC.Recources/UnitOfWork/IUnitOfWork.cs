namespace VC.Recources.Domain.UnitOfWork;

public interface IUnitOfWork
{
    public Task SaveAsync();
}