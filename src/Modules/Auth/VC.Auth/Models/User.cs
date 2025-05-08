using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace VC.Auth.Models;

public class User
{
    public User()
    {
    }
    
    public User(Guid userId, string email, string password)
    {
        UserId = userId;
        Email = email;
        Password = password;
    }

    public Guid UserId { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string Salt { get; set; }

    public static User Create(Guid userId, string email, string password)
        => new User(userId, email, password);
}