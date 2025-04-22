namespace VC.MailkitIntegration;

public class MailSenderInfo
{
    /// <summary>
    /// Имя Отправителя
    /// </summary>
    public string SenderMailName { get; set; }

    /// <summary>
    /// Пароль для аутентификации в api отправки сообщений
    /// </summary>
    public string SenderAppPassword { get; set; }

    /// <summary>
    /// Хост отправителя
    /// </summary>
    public string SmtpHost { get; set; }
}