namespace VC.Orders.Repositories;

public interface IOutboxMessagesRepository
{
    public const int BatchSize = 100;

    public Task<List<OutboxMessage>> GetUnprocessedByContentTypeAsync(string contentTypeFullName);

    public Task UpdateAsync(OutboxMessage message);
}