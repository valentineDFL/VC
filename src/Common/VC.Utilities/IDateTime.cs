namespace VC.Utilities;

public interface IDateTime
{
    public System.DateTime UtcNow { get; }
}

public class DateTimeProvider : IDateTime
{
    public DateTime UtcNow => System.DateTime.UtcNow;
}