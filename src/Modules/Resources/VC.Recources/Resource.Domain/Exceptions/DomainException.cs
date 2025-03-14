namespace VC.Recources.Resource.Domain.Exceptions;

public class DomainException : Exception
{
    public DomainException() { }

    public DomainException(string message)
        : base(message) { }

    public DomainException(string message, Exception exception)
        : base(message, exception) { }
}
