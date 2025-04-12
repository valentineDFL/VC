using VC.Utilities.MailSend;

namespace VC.Tenants.Api.Endpoints;

internal static class TenantEmailVerifyForm
{
    public static Message RegistrationTenantEmailVerifyForm(string link, string receiverName, string receiverMail)
    {
        var subject = "Регистрация на сайте";
        string header = "Спасибо за регистрацию на сайте!";

        var linkTag = $"<a href= \"{link}\">Verify Link</a>";
        var text = $"Вы зарегестрировались на нашем сайте, но для возможности иметь все возможности при использовании ресурса вам необходимо подтвердить вашу почту, для этого вам нужно перейти по ссылке: {linkTag}";

        return new Message(subject, text, receiverName, receiverMail, header);
    }

    public static Message VerifyTenantEmailForm(string link, string receiverName, string receiverMail)
    {
        var subject = "Подтверждение почты";
        string header = "Вы не подтвердили почту!";

        var linkTag = $"<a href= \"{link}\">Verify Link</a>";
        var text = $"Вы зарегестрировались на нашем сайте, но для возможности иметь все возможности при использовании ресурса вам необходимо подтвердить вашу почту, для этого вам нужно перейти по ссылке: {linkTag}";

        return new Message(subject, text, receiverName, receiverMail, header);
    }
}