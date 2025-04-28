using VC.MailkitIntegration;

namespace VC.Tenants.Application;

internal interface ITEnantEmailVerificationFormFactory
{
    public Message GetRegistrationMessageForm(string text, string receiverName, string receiverMail);

    public Message GetVerifyMessageEmailForm(string text, string receiverName, string receiverMail);
}