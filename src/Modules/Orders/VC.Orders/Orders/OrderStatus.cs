using System.Text;
using VC.Orders.Common;

namespace VC.Orders.Orders;

public class OrderStatus : Entity<Guid>
{
    public OrderStatus(Guid id, Guid orderId, OrderState state) : base(id)
    {
        var errors = new StringBuilder();

        if (id == Guid.Empty)
            errors.AppendLine("Id cannot be empty");

        if (orderId == Guid.Empty)
            errors.AppendLine("OrderId cannot be empty");

        if (errors.Length > 0)
            throw new ArgumentException(errors.ToString());

        OrderId = orderId;
        State = state;
        CreatedOnUtc = DateTime.UtcNow;
    }

    public Guid OrderId { get; private set; }

    public OrderState State { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }
}