using FluentResults;

namespace VC.MailkitIntegration;

public interface IMailSender : IDisposable
{
    public Task<Result> SendMailAsync(Message message);
}