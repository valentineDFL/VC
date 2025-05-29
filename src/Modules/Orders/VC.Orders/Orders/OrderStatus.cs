using System.Text;

namespace VC.Orders.Orders;

public class OrderStatus
{
    public OrderStatus(Guid id, Guid orderId, OrderState state)
    {
        var errors = new StringBuilder();

        if (id == Guid.Empty)
            errors.AppendLine("Id cannot be empty");

        if (orderId == Guid.Empty)
            errors.AppendLine("OrderId cannot be empty");

        if (errors.Length > 0)
            throw new ArgumentException(errors.ToString());

        Id = id;
        OrderId = orderId;
        State = state;
        CreatedOnUtc = DateTime.UtcNow;
    }

    protected OrderStatus() { }

    public Guid Id { get; private set; }

    public Guid OrderId { get; private set; }

    public OrderState State { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }
}