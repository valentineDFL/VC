using VC.Tenants.Entities;
using VC.Tenants.Repositories;

namespace VC.Tenants.Infrastructure.Persistence.Repositories;

internal class EmailVerificationRepository : IEmailVerificationRepository
{
    public Task AddAsync(EmailVerification email)
    {
        throw new NotImplementedException();
    }

    public Task<EmailVerification> GetAsync(Guid tenantId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(EmailVerification email)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(EmailVerification email)
    {
        throw new NotImplementedException();
    }
}