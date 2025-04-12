using FluentResults;

namespace VC.Utilities.MailSend;

public interface ISendMailService
{
    public Task<Result<string>> SendMailAsync(Message message);
}