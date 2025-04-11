using VC.Services.Entities;
using static VC.Services.Repositories.IRepositoryBase;

namespace VC.Services.Repositories;
public interface IServiceRepository : IRepositoryBase<Service>
{
    public Task<ICollection<Service>> GetByTenantAsinc(Guid id);
    public Task<Service?> GetAsync();
}
