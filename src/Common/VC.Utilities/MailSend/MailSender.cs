namespace VC.Utilities.MailSend;

public class MailSender
{
    public MailSender(string senderMailName, string senderAppPassword, string smtpHost)
    {
        SenderMailName = senderMailName;
        SenderAppPassword = senderAppPassword;
        SmtpHost = smtpHost;
    }

    /// <summary>
    /// Имя Отправителя
    /// </summary>
    public string SenderMailName { get; private set; }

    /// <summary>
    /// Пароль для аутентификации в api отправки сообщений
    /// </summary>
    public string SenderAppPassword { get; private set; }

    /// <summary>
    /// Хост отправителя
    /// </summary>
    public string SmtpHost { get; private set; }
}