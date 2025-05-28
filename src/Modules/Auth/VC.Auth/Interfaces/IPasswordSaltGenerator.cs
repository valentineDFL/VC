namespace VC.Auth.Interfaces;

public interface IPasswordSaltGenerator
{
    string Generate();
}