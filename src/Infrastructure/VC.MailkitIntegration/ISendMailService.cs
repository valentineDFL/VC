using FluentResults;

namespace VC.MailkitIntegration;

public interface ISendMailService
{
    public Task<Result<string>> SendMailAsync(Message message);
}