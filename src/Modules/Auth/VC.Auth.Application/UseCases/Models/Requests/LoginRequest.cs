namespace VC.Auth.Application.UseCases.Models.Requests;

public record LoginRequest(
    string Username,
    string Password
);