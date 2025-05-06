namespace VC.Utilities;

public interface IDateTime
{
    public DateTime UtcNow { get; }
}

public class SystemDateTimeProvider : IDateTime
{
    public DateTime UtcNow => DateTime.UtcNow;
}