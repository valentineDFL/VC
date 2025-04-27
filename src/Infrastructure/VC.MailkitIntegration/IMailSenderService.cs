using FluentResults;

namespace VC.MailkitIntegration;

public interface IMailSenderService
{
    public Task<Result<string>> SendMailAsync(Message message);
}