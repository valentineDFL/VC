using FluentResults;

namespace VC.Utilities.MailSend;

public interface IMailSenderService
{
    public Task<Result<string>> SendMailAsync(Message message);
}