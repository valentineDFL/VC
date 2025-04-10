namespace VC.Utilities.MailSend;

public class Message
{
    public Message(string subject, string text, string receiverName, string receiverMail, string header, string link)
    {
        Subject = subject;
        Text = text;
        ReceiverName = receiverName;
        ReceiverMail = receiverMail;
        Header = header;
        Link = link;
    }

    /// <summary>
    /// Тема письма
    /// </summary>
    public string Subject { get; private set; }

    /// <summary>
    /// Основное содержание письма
    /// </summary>
    public string Text { get; private set; }

    /// <summary>
    /// Имя получателя
    /// </summary>
    public string ReceiverName { get; private set; }

    /// <summary>
    /// Почта получателя
    /// </summary>
    public string ReceiverMail { get; private set; }

    /// <summary>
    /// Заголовок письма
    /// </summary>
    public string Header { get; private set; }

    /// <summary>
    /// Ссылка на Endpoint
    /// </summary>
    public string? Link { get; private set; }
}