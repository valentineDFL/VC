using VC.MailkitIntegration;

namespace VC.Tenants.Application;

internal class TenantEmailVerifyMessagesFactory : ITEnantEmailVerificationFormFactory
{
    public Message GetRegistrationMessageForm(string code, string receiverName, string receiverMail)
    {
        var subject = "Регистрация на сайте";
        var header = "Спасибо за регистрацию на сайте!";

        var text = $"Вы зарегестрировались на нашем сайте, код подтверждения почты: {code}";

        return new Message(subject, text, receiverName, receiverMail, header);
    }

    public Message GetVerifyMessageEmailForm(string code, string receiverName, string receiverMail)
    {
        var subject = "Подтверждение почты";
        var header = "Вы не подтвердили почту!";

        var text = $"Код подтверждения почты: {code}";

        return new Message(subject, text, receiverName, receiverMail, header);
    }
}