namespace VC.Orders.Application;

public interface IIdempodencyKeyGenerator
{
    public string Generate();
}