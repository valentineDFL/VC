namespace VC.Services.Repositories;

public interface IServicesRepository : IRepository<Service, Guid>
{
    Task<bool> ExistsAsync(string title, CancellationToken cancellationToken = default);
}