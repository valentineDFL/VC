using VC.Services.Entities;
using static VC.Services.Repositories.IRepositoryBase;

namespace VC.Services.Repositories;
public interface IServicesRepository : IRepositoryBase<Service>
{
    public Task<ICollection<Service>> GetByTenantAsinc(Guid id);
    public Task<Service?> GetAsync();
    public Task<ICollection<Service>> GetByFilter(string filter, decimal price);
}
