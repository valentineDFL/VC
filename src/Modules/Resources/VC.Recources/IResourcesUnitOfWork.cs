using VC.Utilities;

namespace VC.Recources.Domain;

public interface IResourcesUnitOfWork : IUnitOfWork
{
    IRepository Resources { get; }
}