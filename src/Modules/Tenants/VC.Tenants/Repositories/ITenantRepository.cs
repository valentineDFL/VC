using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VC.Tenants.Models;

namespace VC.Tenants.Repositories
{
    public interface ITenantRepository : IRepositoryBase<Tenant>
    {
        public Task<Tenant> GetByIdAsync(Guid tenantId);
    }
}