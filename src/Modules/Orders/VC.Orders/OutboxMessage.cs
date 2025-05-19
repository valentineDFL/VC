namespace VC.Orders;

public class OutboxMessage
{
    public Guid Id { get; private set; }

    public string ContentType { get; private set; }

    public string Content { get; private set; }

    public DateTime OccuredOnUtc { get; private set; }

    public DateTime? ProcessedOnUtc { get; private set; }

    public string? Error { get; private set; }
}