using VC.Auth.Models;

namespace VC.Auth.Application;

public interface IUserService
{
    void Create(User user);

    void Login(User user);

    void Register(User user);
}