namespace VC.Auth.Application.Models.Requests;

public record RegisterRequest(
    string Email,
    string Password
);