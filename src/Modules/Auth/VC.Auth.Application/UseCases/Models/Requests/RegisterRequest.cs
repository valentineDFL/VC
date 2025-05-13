namespace VC.Auth.Application.UseCases.Models.Requests;

public record RegisterRequest(
    string Username,
    string Password
);