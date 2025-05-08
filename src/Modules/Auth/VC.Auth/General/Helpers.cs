using System.Transactions;

namespace VC.Auth.General;

public class Helpers
{
    public static TransactionScope CreateTransactionScope(int seconds = 6000)
        => new TransactionScope(
            TransactionScopeOption.Required,
            new TimeSpan(0, 0, seconds),
            TransactionScopeAsyncFlowOption.Enabled
        );
}