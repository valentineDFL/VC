using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VC.Tenants.Repositories;

namespace VC.Tenants.Infrastructure.Persistence.Repositories
{
    internal class TenantRepository : ITenantRepository
    {
        private readonly TenantsDbContext _dbContext;

        public TenantRepository(TenantsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Tenant entity)
        {
            await _dbContext.Tenants.AddAsync(entity);
        }

        public async Task<Tenant> GetByIdAsync(Guid tenantId)
        {
            return await _dbContext.Tenants.FirstOrDefaultAsync(t => t.Id == tenantId);
        }

        public void Remove(Tenant entity)
        {
            _dbContext.Remove(entity);
        }

        public void Update(Tenant entity)
        {
            _dbContext.Tenants.Update(entity);
        }
    }
}