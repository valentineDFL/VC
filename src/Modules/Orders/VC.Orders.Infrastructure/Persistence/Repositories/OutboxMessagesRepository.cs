using Microsoft.EntityFrameworkCore;
using VC.Orders.Repositories;

namespace VC.Orders.Infrastructure.Persistence.Repositories;

internal class OutboxMessagesRepository : IOutboxMessagesRepository
{
    private readonly OrdersDbContext _dbContext;
    private readonly DbSet<OutboxMessage> _messages;

    public OutboxMessagesRepository(OrdersDbContext dbContext)
    {
        _dbContext = dbContext;
        _messages = _dbContext.OutboxMessages;
    }

    public async Task<List<OutboxMessage>> GetUnprocessedByContentTypeAsync(string contentTypeFullName)
    {
        return await _messages.Where(om => om.ProcessedOnUtc == null && om.ContentType == contentTypeFullName)
            .OrderBy(om => om.OccuredOnUtc)
            .ToListAsync();
    }

    public Task UpdateAsync(OutboxMessage message)
    {
        _messages.Update(message);

        return Task.CompletedTask;
    }
}