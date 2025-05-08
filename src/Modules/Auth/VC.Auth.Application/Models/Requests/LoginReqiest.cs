namespace VC.Auth.Application.Models.Requests;

public record LoginReqiest(
    string Email,
    string Password
);