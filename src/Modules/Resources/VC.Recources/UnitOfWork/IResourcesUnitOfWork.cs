using VC.Utilities;

namespace VC.Recources.Domain.UnitOfWork;

public interface IResourcesUnitOfWork : IUnitOfWork
{
    IRepository Resources { get; }
}