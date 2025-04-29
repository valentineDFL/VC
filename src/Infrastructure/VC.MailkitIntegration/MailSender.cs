using FluentResults;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace VC.MailkitIntegration;

internal class MailSender : IMailSender
{
    private SmtpClient _smtpClient;
    private MailSenderInfo _mailSender;

    public MailSender(IOptions<MailSenderInfo> options)
    {
        _mailSender = options.Value;
        _smtpClient = new SmtpClient();
        _smtpClient.Connect(_mailSender.SmtpHost, 587, MailKit.Security.SecureSocketOptions.StartTls);
        _smtpClient.Authenticate(_mailSender.SenderMailName, _mailSender.SenderAppPassword);
    }

    public void Dispose()
    {
        _smtpClient.Disconnect(true);
        _smtpClient.Dispose();
    }

    public async Task<Result> SendMailAsync(Message message)
    {
        try
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.Subject = message.Subject;

            mimeMessage.From.Add(new MailboxAddress("Администрация сайта", _mailSender.SenderMailName));
            mimeMessage.To.Add(new MailboxAddress(message.ReceiverName, message.ReceiverMail));

            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = HtmlBodyText.GetBodyText(message.Header, message.Text)
            };

            await _smtpClient.SendAsync(mimeMessage);
            return Result.Ok();
        }
        catch(Exception ex)
        {
            return Result.Fail($"Status: {MailSendingStatus.Fail} Message: {ex.Message}");
        }
    }
}