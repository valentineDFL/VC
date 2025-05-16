﻿namespace VC.Auth.Models;

public class User
{
    public Guid TenantId { get; set; }

    public string Email { get; set; }

    public string Username { get; set; }

    public string PasswordHash { get; set; }
}