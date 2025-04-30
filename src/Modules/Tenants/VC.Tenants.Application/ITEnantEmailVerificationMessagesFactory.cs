using VC.MailkitIntegration;

namespace VC.Tenants.Application;

internal interface ITEnantEmailVerificationMessagesFactory
{
    public Message CreateAfterRegistration(string text, string receiverName, string receiverMail);

    public Message CreateMessageForVerify(string text, string receiverName, string receiverMail);
}