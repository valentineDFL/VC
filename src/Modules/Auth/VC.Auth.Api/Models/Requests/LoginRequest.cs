namespace VC.Auth.Api.Models.Requests;

public record LoginRequest(
    string Email,
    string Password
);