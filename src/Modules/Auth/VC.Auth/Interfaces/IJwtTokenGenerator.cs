namespace VC.Auth.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(Dictionary<string, string> datas);
}