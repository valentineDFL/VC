using FluentResults;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace VC.Utilities.MailSend;

public class MailService : IMailSenderService
{
    private SmtpClient _smtpClient;
    private IOptions<MailSender> _mailSender;

    public MailService(IOptions<MailSender> options)
    {
        _smtpClient = new SmtpClient();
        _smtpClient.Connect(options.Value.SmtpHost, 587, MailKit.Security.SecureSocketOptions.StartTls);
        _smtpClient.Authenticate(options.Value.SenderMailName, options.Value.SenderAppPassword);

        _mailSender = options;
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

            mimeMessage.From.Add(new MailboxAddress("Администрация сайта", _mailSender.Value.SenderMailName));
            mimeMessage.To.Add(new MailboxAddress(message.ReceiverName, message.ReceiverMail));

            mimeMessage.Body = new TextPart
            {
                Text = HtmlBodyText.GetBodyText(message.Header, message.Text, message.Link ?? string.Empty)
            };

            await _smtpClient.SendAsync(mimeMessage);

            //using (var client = new SmtpClient())
            //{
            //    // smtp, sender, program password
            //    await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            //    await client.AuthenticateAsync("zolotuhavalentin@gmail.com", "ejtaajwdjmwzwnvp"); // ejtaajwdjmwzwnvp

            //    await client.SendAsync(emailMessage);

            //    await client.DisconnectAsync(true);
            //}

            return Result.Ok(MailSendStatus.Success.ToString());
        }
        catch(Exception ex)
        {
            return Result.Fail($"Status: {MailSendStatus.Fail} Message: {ex.Message}");
        }
    }
}