namespace VC.Auth.Models;

public class Permission
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public User User { get; set; }
    
    public Guid UserId { get; set; }
}