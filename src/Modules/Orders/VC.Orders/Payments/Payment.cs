using System.Text;
using VC.Orders.Orders;

namespace VC.Orders.Payments;

public class Payment
{
    public Payment(Guid id, Guid orderId, Order order, PaymentState status)
    {
        var errors = new StringBuilder();

        if (id == Guid.Empty)
            errors.AppendLine("Id cannot be empty");

        if (orderId == Guid.Empty)
            errors.AppendLine("OrderId cannot be empty");

        if (order is null)
            errors.AppendLine("Order cannot be null");

        if (errors.Length > 0)
            throw new ArgumentException(errors.ToString());

        Id = id;
        OrderId = orderId;
        Order = order!;
        Status = status;
        CreatedOnUtc = DateTime.UtcNow;
    }

    protected Payment() { }

    public Guid Id { get; private set; }

    public Guid OrderId { get; private set; }

    public Order Order { get; private set; }

    public PaymentState Status { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }
}