namespace VC.Auth.Application.Abstractions;

public interface IJwtTokenValidator
{
    public Task<(bool IsValid, Dictionary<string, object>? Claims)> ValidateAsync(string token);
}