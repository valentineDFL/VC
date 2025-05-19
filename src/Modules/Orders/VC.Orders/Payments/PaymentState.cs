namespace VC.Orders.Payments;

public enum PaymentState
{
    Initialed = 0,
    Canceled = 1,
    Processing = 2,
    Succeeded = 3,
    Refunded = 4,
    Failde = 5,
    PendingRetry = 6
}