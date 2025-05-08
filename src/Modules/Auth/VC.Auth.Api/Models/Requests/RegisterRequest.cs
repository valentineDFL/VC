namespace VC.Auth.Api.Models.Requests;

public record RegisterRequest(
    string Email,
    string Password
);