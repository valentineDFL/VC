using Org.BouncyCastle.Pqc.Crypto.Lms;

namespace VC.Tenants.Application;

internal static class ErrorMessages
{
    public const string TenantNotFound = nameof(TenantNotFound);

    public const string MailNotSent = nameof(MailNotSent);

    public const string TenantHasAlreadyBeenVerified = nameof(TenantHasAlreadyBeenVerified);

    public const string ConfirmationTimeHasExpired = nameof(ConfirmationTimeHasExpired);

    public const string CodesDoesNotEquals = nameof(CodesDoesNotEquals);

    public const string MessageNotSent = nameof(MessageNotSent);
}