using FluentResults;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace VC.Utilities.MailSend;

public class MailService : ISendMailService
{
    private SmtpClient _smtpClient;
    private MailSenderInfo _mailSender;

    public MailService(IOptions<MailSenderInfo> options)
    {
        _mailSender = options.Value;
        _smtpClient = new SmtpClient();
        _smtpClient.Connect(_mailSender.SmtpHost, 587, MailKit.Security.SecureSocketOptions.StartTls);
        _smtpClient.Authenticate(_mailSender.SenderMailName, _mailSender.SenderAppPassword);
    }

    ~MailService()
    {
        _smtpClient.Disconnect(true);
        _smtpClient.Dispose();
    }

    public async Task<Result<string>> SendMailAsync(Message message)
    {
        try
        {
            MimeMessage mimeMessage = new MimeMessage();
            mimeMessage.Subject = message.Subject;

            mimeMessage.From.Add(new MailboxAddress("Администрация сайта", _mailSender.SenderMailName));
            mimeMessage.To.Add(new MailboxAddress(message.ReceiverName, message.ReceiverMail));

            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = HtmlBodyText.GetBodyText(message.Header, message.Text)
            };

            await _smtpClient.SendAsync(mimeMessage);
            return Result.Ok(MailSendStatus.Success.ToString());
        }
        catch(Exception ex)
        {
            return Result.Fail($"Status: {MailSendStatus.Fail} Message: {ex.Message}");
        }
    }
}